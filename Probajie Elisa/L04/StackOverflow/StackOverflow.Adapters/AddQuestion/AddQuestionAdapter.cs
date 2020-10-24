using Primitives.IO;
using StackOverflow.Domain.Schema.Questions.AddQuestionOp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackOverflow.Domain.Schema.Questions.AddQuestionOp.AddQuestionResult;

namespace StackOverflow.Adapters.AddQuestion
{
    public class AddQuestionAdapter : Adapter<AddQuestionCmd, IAddQuestionResult>
    {
        public override Task PostConditions(AddQuestionCmd cmd, IAddQuestionResult result, object state)
        {
            throw new NotImplementedException();
        }

        public override Task<IAddQuestionResult> Work(AddQuestionCmd cmd, object state)
        {
            throw new NotImplementedException();
        }
    }
}
