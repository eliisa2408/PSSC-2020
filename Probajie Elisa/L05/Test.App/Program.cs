using LanguageExt;
using Question.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult.QuestionCreated;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {

            var cmd = new CreateQuestionCmd("Cum functioneaza exceptiile in programarea orientata pe obiecte?"
                , "Am citit despre acestea dar tot nu inteleg"
                + "\nMa poate ajuta cineva cu cateva explicatii? "
                , new string[] { "OOP", "Exceptions" }, "http://elf.cs.pub.ro/poo/laboratoare/exceptii");

            var result = CreateQuestion(cmd);

            result.Match(
                ProcessQuestionCreated,
                ProcessQuestionNotCreated,
                ProcessInvalidQuestion
            );

            Console.ReadLine();
            if (result.form == true)
            {
                Console.WriteLine("Doresti sa votezi acesta intrebare ?");
                Console.WriteLine("yup sau nope ? Y sau N");
                string decision = Console.ReadLine();
                if (decision.Equals("Y"))
                {
                    Console.WriteLine("Poti acorda un vot pozitiv sau negativ: P or N ?");
                    string vote = Console.ReadLine();
                    if (vote.Equals("P"))
                        result.getVotes(1);
                    else if (vote.Equals("N"))
                        result.getVotes(-1);
                }
            }
        }
        private static ICreateQuestionResult ProcessQuestionNotCreated(QuestionNotCreated questionNotCreatedResult)
        {
            Console.WriteLine($"Intrebare creata cu succes: {questionNotCreatedResult.Feedback}");
            return questionNotCreatedResult;
        }

        private static ICreateQuestionResult ProcessQuestionCreated(QuestionCreated question)
        {
            Console.WriteLine($"Intrebare {question.QuestionId}");
            return question;
        }

        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Validarea intrebarii esuata: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        public static ICreateQuestionResult CreateQuestion(CreateQuestionCmd createQuestionCommand)
        {
            if (string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Titlu lipsa" };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Title.Length < 10 && !string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Titlul nu poate sa contina mai putin de 10 caractere." };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Title.Length > 180)
            {
                var errors = new List<string>() { "Titlul nu poate avea mai mult de 200 de caractere." };
                return new QuestionValidationFailed(errors);
            }

            if (string.IsNullOrWhiteSpace(createQuestionCommand.Body))
            {
                var errors = new List<string>() { "Continut lipsa" };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Body.Length < 15 && !string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Continutul nu poate avea mai putin de 15 caractere." };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Body.Length > 10000)
            {
                var errors = new List<string>() { "Continutul nu poate avea mai mult de 1000 caractere. Ati introdus 1001 caractere" };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Tags.Length < 1)
            {
                var errors = new List<string>() { "Introduceti cel putin un tag" };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Tags.Length > 10)
            {
                var errors = new List<string>() { "Ati introdus prea multe taguri" };
                return new QuestionValidationFailed(errors);
            }

            if (new Random().Next(10) > 1)
            {
                return new QuestionNotCreated("Intrebarea nu s-a putut valida");
            }

            var questionId = Guid.NewGuid();
            var result = new QuestionCreated(questionId, createQuestionCommand.Title, "probajieelisa@gmail.com", true);

            if (result.form)
            {
                Console.WriteLine(result.ToString());
            }
            else
            {
                QuestionNotCreated feedback = new QuestionNotCreated("Intrebarea nu s-a creat !");
            }

            return result;
        }
    }
}

