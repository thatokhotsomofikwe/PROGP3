namespace PROGP3
{
    public class QuizQuestion
    {
        public required string Question { get; set; }

        public required string[] Options { get; set; }

        public int CorrectIndex { get; set; }

        public required string Explanation { get; set; }

        public bool IsTrueFalse { get; set; }
    }
}