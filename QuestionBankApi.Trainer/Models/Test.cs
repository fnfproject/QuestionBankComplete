﻿using System;
using System.Collections.Generic;

namespace QuestionBankApi.Trainer.Models;

public partial class Test
{
    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public string Hyperlinks { get; set; } = null!;

    public int? TestMaxMarks { get; set; }

    public int? TestNoOfQuestions { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime ExpiryTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual UserTbl? CreatedByNavigation { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<TestPaper> TestPapers { get; set; } = new List<TestPaper>();

    public virtual ICollection<TestTrainee> TestTrainees { get; set; } = new List<TestTrainee>();
}
