using UnityEngine;

namespace TinyBitTurtle
{
    public partial class loadSaveCtrl
    {
        public partial class GameSaveData
        {
            [HideInInspector]
            public int level;

            [HideInInspector]
            public int score;

            [HideInInspector]
            public int highScore;
        }

        

        //
        public void Save()
        {
            // set values into gameData class
            //gameData.level = 0;
            //gameData.score = 0;
            //gameData.highScore = 0;

            //// get the save string from the serialized class
            //SaveGameData();
        }

        public void Load()
        {
            // get the save string from the serialized class
            //LoadGameData();

            //// set values into the game
            //gameData.level = 0;
            //gameData.score = 0;
            //gameData.highScore = 0;
        }
    }
}


