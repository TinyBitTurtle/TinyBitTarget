namespace TinyBitTurtle
{
    public partial class ScoreCtrl
    {
        private int numArrows;

        public UILabel scoreUI;
        public UILabel hiScoreUI;
        public UILabel numArrowsUI;

        public void addScore(int _Score)
        {
            UpdateScore(_Score);

            // display user scores
            if (scoreUI)
                scoreUI.text = Score.ToString();

           if (hiScoreUI)
                hiScoreUI.text = HiScore.ToString();

            if (numArrowsUI)
                numArrowsUI.text = numArrows.ToString();
        }
    }
}