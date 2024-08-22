namespace TheatricalPlayersRefactoringKata.Models
{
    public class Performance
    {
        public int Id { get; set; } // Adicione esta linha se necess�rio
        public int PlayId { get; set; }
        public Play Play { get; set; }
        public int Seats { get; set; }

        // Construtor com par�metros
        public Performance(int playId, int seats)
        {
            PlayId = playId;
            Seats = seats;
        }

        // Construtor padr�o
        public Performance() { }
    }
}
