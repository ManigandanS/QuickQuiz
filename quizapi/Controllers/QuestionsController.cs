﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QuickQuiz.Domain;
using quizapi.Models;

namespace quizapi.Controllers
{
    public class QuestionsController : ApiController
    {
        private quizapiContext db = new quizapiContext();

        // GET: api/Questions
        public IQueryable<Question> GetQuestions()
        {
            return db.Questions;
        }

        // GET: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult GetQuestion(string id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(string id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.questionId)
            {
                return BadRequest();
            }

            db.Entry(question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Questions
        [ResponseType(typeof(Question))]
        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Questions.Add(question);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (QuestionExists(question.questionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = question.questionId }, question);
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(string id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            db.Questions.Remove(question);
            db.SaveChanges();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(string id)
        {
            return db.Questions.Count(e => e.questionId == id) > 0;
        }
    }
}