﻿using QuickQuiz.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickQuiz.Data.Configuration
{
    public class AnswerConfiguration:EntityTypeConfiguration<Answer>
    {
        public AnswerConfiguration()
        {
            ToTable("Answers");
            HasKey(r => r.AnswerId);
            HasRequired(qu => qu.Question)
                .WithMany(A =>A.Answers)
                .HasForeignKey(A=>A.QuestionId)
                .WillCascadeOnDelete(true);

            //tous les champs monquent les proprietés sur les attribut dans la base de donnée
        }
    }
}
