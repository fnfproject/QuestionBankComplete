﻿namespace BlazorTrainee.Dtos
{
    public class QuestionDto
    {
        public string Subject { get; set; }
        public string Topic { get; set; }
        public string DifficultyLevel { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
        public int CreatedBy { get; set; }
    }
}
