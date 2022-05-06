using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserTestManager : IUserTestService
    {
        IUserTestDal _userTestDal;
        IVoteLimitDal _voteLimitDal;
        IUserInfoDal _userInfoDal;
        


        public UserTestManager(IUserTestDal userTestDal,IVoteLimitDal voteLimitDal,IUserInfoDal userInfoDal)
        {
            _userTestDal = userTestDal;
            _voteLimitDal = voteLimitDal;
            _userInfoDal = userInfoDal;
            
        }

        public IResult Add(UserTest userTest)
        {

            if (SetVoteLimitAndAdd(userTest).Success)
            {
                _userTestDal.Add(userTest);
                return new SuccessResult(Messages.VoteSuccess);
            }
            else if (FindLastDataMinusVoteLimitAndAdd(userTest).Success)
            {
                _userTestDal.Add(userTest);
                return new SuccessResult(Messages.VoteSuccess);
            }
            else
            {
                return new SuccessResult(Messages.VoteFailed);
            }

        }
        private IResult SetVoteLimitAndAdd(UserTest userTest)
        {
            userTest.Date = DateTime.Now;
            //userTest.VoteDate= DateTime.Now;
            var result = _userTestDal.GetAll(x => x.UserId == userTest.UserId && x.VoteDate == userTest.VoteDate).ToList();
            if (result.Count == 0)
            {
                var limit = _voteLimitDal.GetAll().FirstOrDefault().Limit;

                userTest.VoteLimit = limit - 1;


            }
            else
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IResult FindLastDataMinusVoteLimitAndAdd(UserTest userTest)
        {
            userTest.Date = DateTime.Now;
            //userTest.Date=DateTime.Now;
            var result = _userTestDal.GetAll(x => x.UserId == userTest.UserId && x.VoteDate == userTest.VoteDate).ToList();
            if (result.FindLast(x => x.UserId == userTest.UserId && x.VoteDate == userTest.VoteDate).VoteLimit > 0)
            {
                userTest.VoteLimit = result.FindLast(x => x.UserId == userTest.UserId && x.VoteDate == userTest.VoteDate).VoteLimit - 1;

            }
            else
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }


        public IDataResult<List<UserTest>> GetAll()
        {
            return new SuccessDataResult<List<UserTest>>(_userTestDal.GetAll(), Messages.Listed);
        }

        public IResult Update(UserTest userTest)
        {
            throw new NotImplementedException();
        }
    }
}
