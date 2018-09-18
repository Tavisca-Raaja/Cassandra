using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class GameProccess
    {
        static List<string> Participant = new List<string>();
        static int[,] tictactoe = new int[3,3];
        static int row = 0, column = 0,occupied=0;
        static string gameResult="";
        static string previousPlayer = "";


        public bool StartPlay(string player1AccessKey,string player2AccessKey)
        {
            if (Participant.Count() == 0)
            {
                Participant.Add(player1AccessKey);
                Participant.Add(player2AccessKey);
                previousPlayer = Participant[1];
            }
            else
                return false;
            return true;
        }
        public bool checkGameStartedOrNot()
        {
            if (Participant.Count() == 0)
                return false;
            return true;
        }
       
        public string updateMoves(string accessKey,string position)
        {
            if (previousPlayer.Equals(accessKey))
                return "Invalid move,it is not your turn";
            int.TryParse(position[0].ToString(), out row);
            int.TryParse(position[1].ToString(), out column);

            if (tictactoe[row,column]==0)
            {
                if (accessKey == Participant[0])
                 tictactoe[row, column] = 1;
                else if (accessKey == Participant[1])
                 tictactoe[row, column] = 2;
                occupied++;
            }
            else
             return "Invalid Move,already occupied";
            previousPlayer = accessKey;
            gameResult = checkGameStatus();
            return gameResult;
        }

        public bool validateAccesskey(string accessKey)
        {
            if (!Participant.Contains(accessKey))
                return false;
               return true;
        }

        public bool checkInProccessOrNot()
        {
            if (checkGameStatus() == "Match InProgress")
                return true;
            return false;
        }

        public string checkGameStatus()
        {
            if (Participant.Count() == 0)
                return "Match Not started yet";
            for(int player=0;player<2;player++)
            {
                //row-wise check
                for(int rowNumber=0;rowNumber<3;rowNumber++)
                {
                    if(tictactoe[rowNumber,0]==player+1 && tictactoe[rowNumber,1]==player+1 && tictactoe[rowNumber,2]==player+1)
                        return (string.Format("winner is player {0}",player+1));
                }

                //column-wise
                for(int columnNumber=0;columnNumber<3;columnNumber++)
                {
                    if (tictactoe[0, columnNumber] == player + 1 && tictactoe[1, columnNumber] == player + 1 && tictactoe[2, columnNumber] == player + 1)
                        return (string.Format("winner is player {0}", player+1));
                }
                if((tictactoe[0,0]==player+1 && tictactoe[1,1]==player+1 && tictactoe[2,2]==player+1) || (tictactoe[2,0]==player+1 && tictactoe[1,1]==player+1 && tictactoe[0,2]==player+1))
                    return (string.Format("winner is player {0}", player+1));
            }
            if(occupied==9)
            return "Match-Tie";

            return "Match InProgress";
        }

    }
}
