﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle
{
    private CastleRoom[,] rooms;

    private Vector2Int oldRoomPosition;
    private Vector2Int actualMainRoomPosition;
    private List<Vector2Int> allowedGenerationDirection = new List<Vector2Int>();
    private List<int> keysToInstall = new List<int>();
    private int keyRemaining;

    private int blockMainPath = 0;//la valeur de cette variable est inversement proportionnelle aux chances de bloquer le chemin principale

    public Castle(int castleWidth, int castleHeight, int annexLockedDoorNbr)
    {
        keyRemaining = annexLockedDoorNbr;
        //initialisation des directions de generations possibles
        allowedGenerationDirection.Add(Vector2Int.left);
        allowedGenerationDirection.Add(Vector2Int.up);
        allowedGenerationDirection.Add(Vector2Int.right);
        allowedGenerationDirection.Add(Vector2Int.down);

        rooms = new CastleRoom[castleWidth, castleHeight];
        //remplis le chateau de pieces
        for (int x = 0; x < castleWidth; x++)
        {
            for (int y = 0; y < castleHeight; y++)
            {
                rooms[x, y] = new CastleRoom();
            }
        }

        actualMainRoomPosition = new Vector2Int(castleWidth / 2, 0);//commencer la generation au "milieu" du chateau
        rooms[actualMainRoomPosition.x, actualMainRoomPosition.y].Generate(keysToInstall);
        while (GenerateMainPath(castleWidth, castleHeight))
        {
            GenerateAnnexPath(castleWidth, castleHeight);
        }
    }

    public CastleRoom GetRoom(Vector2Int index)
    {
        return rooms[index.x, index.y];
    }

    public CastleRoom GetRoom(int xIndex, int yIndex)
    {
        return rooms[xIndex, yIndex];
    }

    /// <summary>
    /// Génère la prochaine piece du chemin principale
    /// </summary>
    /// <returns>retourne true par défaut et false si la génération du chemin est terminée</returns>
    private bool GenerateMainPath(int castleWidth, int castleHeight)
    {
        Debug.Log("room en cours de création:" + actualMainRoomPosition);
        bool isRoomGenerated = false;

        while (!isRoomGenerated)//tant qu'aucune nouvelle pièce n'a été crée:
        {
            Debug.Log("nbr de directions possibles:");
            Debug.Log(allowedGenerationDirection.Count);
            //choisis une direction au hasard
            Vector2Int generationDirection = HomeMadeFunctions.GetRandom(allowedGenerationDirection);
            Vector2Int nextRoomPosition = generationDirection + actualMainRoomPosition;
            if (IsInBounds(nextRoomPosition, castleWidth, castleHeight) && !(rooms[nextRoomPosition.x, nextRoomPosition.y].isGenerated))
            //si la future pièce ne dépasse pas les limites du chateau et si la piece n'a pas déjà été générée:
            {
                //a de chances de bloquer le chemin principale si nécessaire
                if (blockMainPath != 0 && Constants.RANDOM.NextDouble() > 1f / (blockMainPath) / 1.10f)
                {
                    //bloque le chemin principale
                    rooms[actualMainRoomPosition.x, actualMainRoomPosition.y].CreateLockedDoor(-1, generationDirection);
                    Debug.Log("chemin principale bloqué");
                    blockMainPath--;
                }
                else
                {
                    rooms[actualMainRoomPosition.x, actualMainRoomPosition.y].CreateHoleExit(generationDirection);
                }

                //créé la prochaine pièce et la relie avec l'actuelle
                rooms[nextRoomPosition.x, nextRoomPosition.y].CreateHoleExit(generationDirection * (-1));
                rooms[nextRoomPosition.x, nextRoomPosition.y].Generate(keysToInstall);

                Debug.Log("sortie de " + actualMainRoomPosition + ":" + rooms[actualMainRoomPosition.x, actualMainRoomPosition.y].ToString());

                oldRoomPosition = actualMainRoomPosition;
                actualMainRoomPosition = nextRoomPosition;
                //re-remplis la liste des directions possible
                allowedGenerationDirection = new List<Vector2Int>();
                allowedGenerationDirection.Add(Vector2Int.left);
                allowedGenerationDirection.Add(Vector2Int.up);
                allowedGenerationDirection.Add(Vector2Int.right);
                allowedGenerationDirection.Add(Vector2Int.down);

                //empeche la génération de revenir en arrière en interdisant cette direction
                allowedGenerationDirection.Remove(generationDirection * (-1));

                Debug.Log("direction impossible:" + (generationDirection * -1));

                isRoomGenerated = true;
            }
            else
            //sinon on interdit d'aller dans cette direction et on repart pour un tour de boucle
            {
                allowedGenerationDirection.Remove(generationDirection);
                Debug.Log("direction impossible:" + (generationDirection));
                isRoomGenerated = false;
            }
            if (allowedGenerationDirection.Count == 0)
            //si aucune direction n'est possible
            {
                Debug.Log("generation du chemin principale finie!");
                Debug.Log("sortie de " + actualMainRoomPosition + ":" + rooms[actualMainRoomPosition.x, actualMainRoomPosition.y].ToString());
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// A 1/3 chance de générer une pièce annexe au chemin principale dans une direction aléatoire.
    /// Si la pièce est déjà générée, on crée une porte à sens unique pour la rejoindre.
    /// </summary>
    /// <returns>
    /// true si une piece annexe ou une porte a sens unique a été crée, sinon false.
    /// </returns>

    private bool GenerateAnnexPath(int castleWidth, int castleHeight)
    {
        if (Constants.RANDOM.Next(3) == 0)
        {
            Vector2Int[] possibleDirections = new Vector2Int[] { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };
            Vector2Int choosenDirection = HomeMadeFunctions.GetRandom(possibleDirections);
            Vector2Int annexPathPosition = choosenDirection + actualMainRoomPosition;
            Debug.Log("position de la piece annexe: " + annexPathPosition + " " + actualMainRoomPosition);
            if (!IsInBounds(annexPathPosition, castleWidth, castleHeight) || annexPathPosition == oldRoomPosition)
            //si la piece annexe est en dehors des limites du chateau ou qu'elle retourne sur les pas de la génération du chemin principale
            {
                return false;
            }
            CastleRoom AnnexRoom = rooms[annexPathPosition.x, annexPathPosition.y];

            if (AnnexRoom.isGenerated)
            {
                Debug.Log("porte sens unique créé en position:" + actualMainRoomPosition);
                rooms[actualMainRoomPosition.x, actualMainRoomPosition.y].CreateOneWayDoor(true, choosenDirection);
                //Castle[annexPathPosition.x, annexPathPosition.y].CreateOneWayDoor(false, choosenDirection);
            }
            else
            {
                GenerateAnnexRoom(annexPathPosition);
                AnnexRoom.Generate(keysToInstall);
                Debug.Log("piece annexe crée: " + annexPathPosition);
            }
            return true;
        }
        return false;
    }

    private void GenerateAnnexRoom(Vector2Int annexRoomPosition)
    {
        CastleRoom actualMainPathRoom = rooms[actualMainRoomPosition.x, actualMainRoomPosition.y];
        CastleRoom annexRoom = rooms[annexRoomPosition.x, annexRoomPosition.y];
        Vector2Int exitLocation = actualMainRoomPosition - annexRoomPosition;
        //mets une clef dans la pièce annexe qui ouvre la porte qui empeche de continuer le chemin principale
        if (Constants.RANDOM.Next(blockMainPath + 2) == 0)//plus on l'a bloqué moins il y a de chance de le bloquer
        {
            //relie la piece annex au chemin principale
            annexRoom.CreateHoleExit(exitLocation);
            actualMainPathRoom.CreateHoleExit(exitLocation * -1);
            annexRoom.keys.Add(-1);//le clef qui ouvrent le chemin principale ont toutes l'ID -1
            blockMainPath++;
        }
        //ferme à clef la pièce annexe et met la clef dans une liste de clef à poser pendant la génération et pose un coffre
        else if (keyRemaining >= 0)
        {
            Vector2Int lockedDoorLocation = actualMainRoomPosition - annexRoomPosition;
            annexRoom.CreateLockedDoor(keyRemaining, lockedDoorLocation);
            keysToInstall.Add(keyRemaining);
            keyRemaining--;
            annexRoom.chest = true;
        }
    }
    private bool IsInBounds(Vector2Int positionToTest, int castleWidth, int castleHeight)
    {
        return positionToTest.x >= 0 && positionToTest.y >= 0 && positionToTest.x < castleWidth && positionToTest.y < castleHeight;
    }
}
