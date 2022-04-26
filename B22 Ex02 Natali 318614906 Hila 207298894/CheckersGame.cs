using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class CheckersGame
    {
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;

        public CheckersGame(Player io_Player1, Player io_Player2, Board io_Board)
        {
            m_Board = io_Board;
            m_Player1 = io_Player1;
            m_Player2 = io_Player2;
        }

        public Board CheckersBoard
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public Player CheckerPlayer1
        {
            get { return m_Player1; }
            set { m_Player1 = value; }
        }

        public Player CheckerPlayer2
        {
            get { return m_Player2; }
            set { m_Player2 = value; }
        }

        private static bool CheckIfTheWantedPositionIsMustEatenPosition(List<List<int>> i_PossiblePositionToEat, List<int> i_WantedPosition)
        {
            bool returnAnswer = false;
            for (int i = 0; i < i_PossiblePositionToEat.Count; i++)
            {
                if (i_PossiblePositionToEat[i][0] == i_WantedPosition[0] && i_PossiblePositionToEat[i][1] == i_WantedPosition[1]
                    && i_PossiblePositionToEat[i][2] == i_WantedPosition[2] && i_PossiblePositionToEat[i][3] == i_WantedPosition[3])
                {
                    returnAnswer = true;
                    i = i_PossiblePositionToEat.Count;
                }
            }

            return returnAnswer;
        }

        public void CheckAndHandlePlayersAfterOneOfThePlayersDosentHaveValidMove(int i_IndexWhoEat, bool i_Draw, ref bool i_EndGame, ref int o_Index)
        {
            if (i_IndexWhoEat == 1)
            {
                if (!i_Draw)
                {
                    m_Player2.UpdatePointsAfterEachGame(m_Player1, m_Board);
                }
            }
            else
            {
                if (!i_Draw)
                {
                    m_Player1.UpdatePointsAfterEachGame(m_Player2, m_Board);
                }
            }
        }

        public void UpdatePlayersAfterOneOfThemGotEaten(ref bool i_EndGame, int i_IndexWhoEat, ref bool i_PlayerWithoutPieces, ref int io_Index)
        {
            CheckWhowEatAndUpdate(ref i_EndGame, i_IndexWhoEat);
            if (i_EndGame == true)
            {
                i_PlayerWithoutPieces = true;
                if (i_IndexWhoEat == 1)
                {
                    m_Player1.UpdatePointsAfterEachGame(m_Player2, m_Board);
                }
                else
                {
                    m_Player2.UpdatePointsAfterEachGame(m_Player1, m_Board);
                }
            }
        }

        public void IfThePlayerDosentWantToQuitContinueTheGame(bool i_Quit, Player i_CurrPlayer, Player i_NextPlayer, ref bool i_IsEaten, ref int i_Index, ref int i_IndexWhoEat, ref bool i_EndGame, ref bool i_Draw, List<int> i_UserMoverInInt, ref string i_UserMoveInString, List<List<int>> i_ListOfPositionOptionToEat)
        {
            if (!i_Quit)
            {
                CheckPositionAndMove(i_CurrPlayer, i_NextPlayer, i_UserMoverInInt, ref i_IsEaten, ref i_Index, i_IndexWhoEat, i_ListOfPositionOptionToEat, i_UserMoveInString);
                CheckIfDrawOrIfSomeoneCantMove(ref i_EndGame, ref i_Draw, i_CurrPlayer, i_NextPlayer);
            }
        }

        private void UpdatePlayerIfKing(Player i_Player, List<int> i_Position)
        {
            int numberOfPlayer = i_Player.NumberOfPlayer;
            if (numberOfPlayer == 1)
            {
                if (i_Position[3] == m_Board.BoardSize - 1)
                {
                    m_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.IsKing = true;
                    m_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.SignOfPlayerInBoard = 'U';
                }
            }
            else
            {
                if (i_Position[3] == 0)
                {
                    m_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.IsKing = true;
                    m_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.SignOfPlayerInBoard = 'K';
                }
            }
        }

        public void InitNewGame()
        {
            int sizeOfBoard = m_Board.BoardSize;
            m_Player1.RemainPieces = ((sizeOfBoard * sizeOfBoard) - (2 * sizeOfBoard)) / 4;
            m_Player2.RemainPieces = ((sizeOfBoard * sizeOfBoard) - (2 * sizeOfBoard)) / 4;
            m_Player1.LastMove = string.Empty;
            m_Player2.LastMove = string.Empty;
        }

        private void GivePointsToTheWinner(Player i_LoserPlayer, Player i_WinnerPlayer)
        {
            int piecesOfLoserPlayer = i_LoserPlayer.RemainPieces, piecesOfWinnerPlayer = i_WinnerPlayer.RemainPieces;
            i_WinnerPlayer.PointsOfPlayer += piecesOfWinnerPlayer - piecesOfLoserPlayer;
        }

        public void GetRandomPosition(List<int> io_FromWhereToWhereToEatAndMove, List<List<int>> io_ListOfPositionOptionToEat, ref string io_UserInString)
        {
            List<List<int>> listOfPositionOptionToMove = new List<List<int>>();
            int randomPos;
            bool someoneToEat = CheckIfThereIsAnyoneToEatAndReturnOptions(m_Player2, io_ListOfPositionOptionToEat);
            Random rand = new Random();

            if (someoneToEat == true)
            {
                randomPos = rand.Next(io_ListOfPositionOptionToEat.Count() - 1);
                UpdateArrOfPosition(io_FromWhereToWhereToEatAndMove, io_ListOfPositionOptionToEat[randomPos][1], io_ListOfPositionOptionToEat[randomPos][0], io_ListOfPositionOptionToEat[randomPos][3], io_ListOfPositionOptionToEat[randomPos][2]);
            }
            else
            {
                UpdateOptionsOfValidMovements(m_Player2, listOfPositionOptionToMove, 2);
                randomPos = rand.Next(listOfPositionOptionToMove.Count() - 1);
                UpdateArrOfPosition(io_FromWhereToWhereToEatAndMove, listOfPositionOptionToMove[randomPos][1], listOfPositionOptionToMove[randomPos][0], listOfPositionOptionToMove[randomPos][3], listOfPositionOptionToMove[randomPos][2]);
            }

            io_UserInString = ChangeIntToString(io_FromWhereToWhereToEatAndMove);
        }

        public void UpdateOptionsOfValidMovements(Player i_Player, List<List<int>> io_ListOfPositionOptionToMove, int i_NumberOfPlayer)
        {
            int currRow, currCol, direction = -1;
            if (i_NumberOfPlayer == 1)
            {
                direction = 1;
            }

            for (int i = 0; i < i_Player.Positions.Count; i++)
            {
                currRow = i_Player.Positions[i].X;
                currCol = i_Player.Positions[i].Y;
                if (Board.CheckThatWeDontGoBeyondBoundaries(m_Board.BoardSize, currRow + direction, currCol - 1) &&
                    m_Board.GameBoard[currRow + direction, currCol - 1].IsEmpty)
                {
                    List<int> possiblePosition = new List<int>(4);
                    UpdateArrOfPosition(possiblePosition, currRow, currCol, currRow + direction, currCol - 1);
                    io_ListOfPositionOptionToMove.Add(possiblePosition);
                }

                if (Board.CheckThatWeDontGoBeyondBoundaries(m_Board.BoardSize, currRow + direction, currCol + 1) &&
                    m_Board.GameBoard[currRow + direction, currCol + 1].IsEmpty)
                {
                    List<int> possiblePosition = new List<int>(4);
                    UpdateArrOfPosition(possiblePosition, currRow, currCol, currRow + direction, currCol + 1);
                    io_ListOfPositionOptionToMove.Add(possiblePosition);
                }
            }
        }

        private void CheckIfDrawOrIfSomeoneCantMove(ref bool o_EndGame, ref bool o_Draw, Player i_CurrPlayer, Player i_NextPlayer)
        {
            bool validMovesOfCurrPlayer, validMovesOfNextPlayer;
            validMovesOfCurrPlayer = i_CurrPlayer.CheckIfThereIsNoValidMoveForPlayer(this);
            validMovesOfNextPlayer = i_NextPlayer.CheckIfThereIsNoValidMoveForPlayer(this);

            if (validMovesOfCurrPlayer && validMovesOfNextPlayer)
            {
                o_Draw = true;
                o_EndGame = true;
            }
            else if (validMovesOfCurrPlayer)
            {
                o_EndGame = true;
            }
            else if (validMovesOfNextPlayer)
            {
                o_EndGame = true;
            }
        }

        private void UpdateArrOfPosition(List<int> io_FromWhereToWhereToMove, int i_CurrRowNum, int i_CurrColNum, int i_NextRowNum, int i_NextColNum)
        {
            io_FromWhereToWhereToMove.Clear();
            io_FromWhereToWhereToMove.Add(i_CurrColNum);
            io_FromWhereToWhereToMove.Add(i_CurrRowNum);
            io_FromWhereToWhereToMove.Add(i_NextColNum);
            io_FromWhereToWhereToMove.Add(i_NextRowNum);
        }

        private void CheckWhowEatAndUpdate(ref bool io_endGame, int i_NumberOfPlayer)
        {
            if (i_NumberOfPlayer == 1)
            {
                UpdatePlayersAfterEaten(m_Player1, m_Player2, ref io_endGame);
            }
            else if (i_NumberOfPlayer == 2)
            {
                UpdatePlayersAfterEaten(m_Player2, m_Player1, ref io_endGame);
            }
        }

        private void CheckPositionAndMove(Player i_CurrPlayer, Player i_NextPlayer, List<int> i_UserMoveInt, ref bool io_IsEaten, ref int i, int i_NumberOfPlayer, List<List<int>> io_PossiblePositionToEat, string i_UserMoveInString)
        {
            bool eatBackWord = false, validEatMove = true;
            bool isThereSomeoneToEat = CheckIfThereIsAnyoneToEatAndReturnOptions(i_CurrPlayer, io_PossiblePositionToEat);
            if (isThereSomeoneToEat)
            {
                validEatMove = CheckIfTheWantedPositionIsMustEatenPosition(io_PossiblePositionToEat, i_UserMoveInt);
            }

            if (!i_CurrPlayer.CheckIfTheCurrPositionIsMine(m_Board, i_UserMoveInt, ref io_IsEaten, ref eatBackWord) || !validEatMove)
            {
                i--;
            }
            else
            {
                UpdateLastValidMoveForPlayer(i_UserMoveInString, i_CurrPlayer);
                i_CurrPlayer.MovePlayerOnBoard(m_Board, i_UserMoveInt, io_IsEaten, i_CurrPlayer.NumberOfPlayer, ref eatBackWord);
                UpdateListPointsOfPlayer(i_CurrPlayer, i_NextPlayer, i_UserMoveInt, io_IsEaten);
                UpdatePlayerIfKing(i_CurrPlayer, i_UserMoveInt);
                if (CheckIfThereAreMoreToEatForThatPiece(i_UserMoveInt, i_CurrPlayer.NumberOfPlayer, i_CurrPlayer) && io_IsEaten)
                {
                    i--;
                }
            }
        }

        private void UpdateLastValidMoveForPlayer(string i_UserMoveInString, Player CurrPlayer)
        {
            CurrPlayer.LastMove = i_UserMoveInString;
        }

        private void CheckForEachRemainPieceIfCanEat(ref bool o_ReturnAns, Player i_CurrPlayer, int i_IndexOfPiece, List<List<int>> o_PossiblePositionToEat, int i_Row, int i_Col, int i_NumberOfPlayer)
        {
            bool canEat = false, eat = false;
            if (Board.CheckThatWeDontGoBeyondBoundaries(m_Board.BoardSize, i_CurrPlayer.Positions[i_IndexOfPiece].X + i_Row, i_CurrPlayer.Positions[i_IndexOfPiece].Y + i_Col))
            {
                List<int> position = new List<int> { i_CurrPlayer.Positions[i_IndexOfPiece].Y, i_CurrPlayer.Positions[i_IndexOfPiece].X, i_CurrPlayer.Positions[i_IndexOfPiece].Y + i_Col, i_CurrPlayer.Positions[i_IndexOfPiece].X + i_Row };
                canEat = i_CurrPlayer.CheckIfCanEaten(m_Board, position, i_NumberOfPlayer, ref eat);
                if (canEat)
                {
                    o_PossiblePositionToEat.Add(position);
                    o_ReturnAns = true;
                }
            }
        }

        private bool CheckIfThereIsAnyoneToEatAndReturnOptions(Player i_CurrPlayer, List<List<int>> o_PossiblePositionToEat)
        {
            bool returnAnswer = false, isKing = false;
            o_PossiblePositionToEat.Clear();
            for (int k = 0; k < i_CurrPlayer.RemainPieces; k++)
            {
                isKing = m_Board.GameBoard[i_CurrPlayer.Positions[k].X, i_CurrPlayer.Positions[k].Y].PlayerInBoard.IsKing;
                if (i_CurrPlayer.NumberOfPlayer == 2)
                {
                    CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_CurrPlayer, k, o_PossiblePositionToEat, -2, 2, 2);
                    CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_CurrPlayer, k, o_PossiblePositionToEat, -2, -2, 2);
                    if (isKing)
                    {
                        CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_CurrPlayer, k, o_PossiblePositionToEat, 2, 2, 1);
                        CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_CurrPlayer, k, o_PossiblePositionToEat, 2, -2, 1);
                    }
                }
                else
                {
                    CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_CurrPlayer, k, o_PossiblePositionToEat, 2, 2, 1);
                    CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_CurrPlayer, k, o_PossiblePositionToEat, 2, -2, 1);
                    if (isKing)
                    {
                        CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_CurrPlayer, k, o_PossiblePositionToEat, -2, 2, 2);
                        CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_CurrPlayer, k, o_PossiblePositionToEat, -2, -2, 2);
                    }
                }
            }

            return returnAnswer;
        }

        private void UpdateListPointsOfPlayer(Player io_CurrPlayer, Player io_NextPlayer, List<int> i_FromWhereToWhere, bool i_IsEaten)
        {
            int sizeOfPositionCurr = io_CurrPlayer.RemainPieces, sizeOfPositionNext = io_NextPlayer.RemainPieces;
            int intMoveCol = 0, addOrSub = 0, indexMiddle = 0;
            int backWard = io_CurrPlayer.NumberOfPlayer;
            for (int k = 0; k < sizeOfPositionCurr; k++)
            {
                if (i_FromWhereToWhere[0] == io_CurrPlayer.Positions[k].Y && i_FromWhereToWhere[1] == io_CurrPlayer.Positions[k].X)
                {
                    io_CurrPlayer.Positions[k].Y = i_FromWhereToWhere[2];
                    io_CurrPlayer.Positions[k].X = i_FromWhereToWhere[3];
                    k = sizeOfPositionCurr;
                }
            }

            if (i_IsEaten)
            {
                Player.FindMiddlePosition(i_FromWhereToWhere, ref intMoveCol, ref addOrSub, ref indexMiddle, backWard);
                int colToDelete = i_FromWhereToWhere[0] + intMoveCol;
                int rowToDelete = i_FromWhereToWhere[1] + indexMiddle;
                for (int j = 0; j < sizeOfPositionNext; j++)
                {
                    if (colToDelete == io_NextPlayer.Positions[j].Y && rowToDelete == io_NextPlayer.Positions[j].X)
                    {
                        io_NextPlayer.Positions.RemoveAt(j);
                        j = sizeOfPositionNext;
                    }
                }

                if (backWard == 1)
                {
                    backWard = 2;
                }
                else
                {
                    backWard = 1;
                }

                sizeOfPositionNext = io_NextPlayer.Positions.Count;
                Player.FindMiddlePosition(i_FromWhereToWhere, ref intMoveCol, ref addOrSub, ref indexMiddle, backWard);
                colToDelete = i_FromWhereToWhere[0] + intMoveCol;
                rowToDelete = i_FromWhereToWhere[1] + indexMiddle;
                for (int j = 0; j < sizeOfPositionNext; j++)
                {
                    if (colToDelete == io_NextPlayer.Positions[j].Y && rowToDelete == io_NextPlayer.Positions[j].X)
                    {
                        io_NextPlayer.Positions.RemoveAt(j);
                        j = sizeOfPositionNext;
                    }
                }
            }
        }

        private void UpdatePlayersAfterEaten(Player i_WinPlayer, Player i_LoserPlayer, ref bool o_EndGame)
        {
            o_EndGame = SubPieceOfTheRemainPiece(i_LoserPlayer);
        }

        private bool SubPieceOfTheRemainPiece(Player i_Player)
        {
            bool returnAnswer = false;
            int remainPieces = --i_Player.RemainPieces;
            if (remainPieces == 0)
            {
                returnAnswer = true;
            }

            return returnAnswer;
        }

        private bool CheckIfThereAreMoreToEatForThatPiece(List<int> i_Position, int i_NumberOfPlayer, Player i_Player)
        {
            bool isKing = m_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.IsKing;
            bool returnAnswer = false;
            if (i_NumberOfPlayer == 1)
            {
                returnAnswer = CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(i_Position, i_Player, 1, 1);
            }
            else
            {
                returnAnswer = CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(i_Position, i_Player, -1, 1);
            }

            if (isKing && !returnAnswer)
            {
                if (i_NumberOfPlayer == 1)
                {
                    returnAnswer = CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(i_Position, i_Player, -1, 1);
                }
                else
                {
                    returnAnswer = CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(i_Position, i_Player, 1, 1);
                }
            }

            return returnAnswer;
        }

        private bool CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(List<int> i_Position, Player i_Player, int i_Row, int i_Col)
        {
            bool returnAnswer = false;
            if (Board.CheckThatWeDontGoBeyondBoundaries(m_Board.BoardSize, i_Position[3] + i_Row, i_Position[2] + i_Col) &&
                     m_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] + i_Col].PlayerInBoard.SignOfPlayerInBoard != i_Player.SignOfKing &&
                     m_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] + i_Col].PlayerInBoard.SignOfPlayerInBoard != i_Player.SignOfPlayer &&
                     m_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] + i_Col].PlayerInBoard.SignOfPlayerInBoard != ' ')
            {
                if (Board.CheckThatWeDontGoBeyondBoundaries(m_Board.BoardSize, i_Position[3] + i_Row * 2, i_Position[2] + i_Col * 2) &&
                    m_Board.GameBoard[i_Position[3] + i_Row * 2, i_Position[2] + i_Col * 2].IsEmpty)
                {
                    returnAnswer = true;
                }
            }
            else if (Board.CheckThatWeDontGoBeyondBoundaries(m_Board.BoardSize, i_Position[3] + i_Row, i_Position[2] - i_Col) &&
                     m_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] - i_Col].PlayerInBoard.SignOfPlayerInBoard != i_Player.SignOfKing &&
                     m_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] - i_Col].PlayerInBoard.SignOfPlayerInBoard != i_Player.SignOfPlayer &&
                     m_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] - i_Col].PlayerInBoard.SignOfPlayerInBoard != ' ')
            {
                if (Board.CheckThatWeDontGoBeyondBoundaries(m_Board.BoardSize, i_Position[3] + i_Row * 2, i_Position[2] - i_Col * 2) &&
                    m_Board.GameBoard[i_Position[3] + i_Row * 2, i_Position[2] - i_Col * 2].IsEmpty)
                {
                    returnAnswer = true;
                }
            }

            return returnAnswer;
        }

        private string ChangeIntToString(List<int> i_ComputerMove)
        {
            string returnString = string.Empty;
            StringBuilder builder = new StringBuilder();
            for (int k = 0; k < i_ComputerMove.Count; k++)
            {
                if (k == 2)
                {
                    builder.Append('>');
                }

                if (k % 2 == 0)
                {
                    char col = (char)(i_ComputerMove[k] + 'A');
                    builder.Append(col);
                }
                else
                {
                    char row = (char)(i_ComputerMove[k] + 'a');
                    builder.Append(row);
                }
            }

            returnString = builder.ToString();
            return returnString;
        }
    }
}