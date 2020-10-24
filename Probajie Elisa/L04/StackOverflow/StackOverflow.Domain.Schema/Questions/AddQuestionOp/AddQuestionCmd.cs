using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackOverflow.Domain.Schema.Questions.AddQuestionOp
{
    public class AddQuestionCmd
    {
        public AddQuestionCmd( string title, string body, string tags)
        {
            Title = title;
            Body = body;
            Tags = tags;
        }

        [Required]
        public string Title { get; }
        [Required]
        public string Body { get; }
        [Required]
        public string Tags { get; }
    }
}
