using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflow.Domain.Schema.Questions.AddQuestionOp
{
    [AsChoice]
    public static partial class AddQuestionResult
    {
        public interface IAddQuestionResult { }
        public class QuestionAdded : IAddQuestionResult { }
        public class QuestionNotAdded : IAddQuestionResult { }
    }
}
