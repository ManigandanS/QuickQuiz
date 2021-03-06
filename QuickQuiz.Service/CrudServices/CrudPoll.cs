﻿using QuickQuiz.Data.Infrastructure;
using QuickQuiz.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickQuiz.Service.CrudServices
{
   public class CrudPoll
    {
        DatabaseFactory dbFactory = null;
        UnitOfWork unofWork = null;
        public CrudPoll()
        {
            dbFactory = new DatabaseFactory();
            unofWork = new UnitOfWork(dbFactory);

        }
        public void CreatePoll(Poll P)
        {
            unofWork.PollRepository.Add(P);
            unofWork.Commit();
        }
        public void UpdatePoll(Poll P)
        {
            unofWork.PollRepository.Update(P);
            unofWork.Commit();
        }
        public void DeletePoll(Poll P)
        {
            unofWork.PollRepository.Delete(P);
            unofWork.Commit();
        }
        public Poll GetPoll(int id)
        {
            var P = unofWork.PollRepository.GetById(id);
            return P;
        }
        public IEnumerable<Poll> GetAllPoll()
        {
            return unofWork.PollRepository.GetAll();
        }
        public IEnumerable<Poll> GetPollByQuizManager(string QuizManagerId)
        {
            return unofWork.PollRepository.GetMany(p => p.QuizManager.IdUser == QuizManagerId);
        }
        public void Dispose()
        {
            unofWork.Dispose();
        }
    }
}
