using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VoteManager : IVoteService
    {
        IVoteDal _voteDal;
        IUserInfoDal _userInfoDal;

        public VoteManager(IVoteDal voteDal,IUserInfoDal userInfoDal)
        {
            _voteDal = voteDal;
            _userInfoDal = userInfoDal;
        }
        public IResult Add(Vote vote)
        {
            vote.Date = DateTime.Now;
            var result= _userInfoDal.Get(x=>x.UserId==vote.UserId);
            if (result.VoteLimit>0&&result.VoteDate<=vote.VoteDate)
            {
                _voteDal.Add(vote);
                result.VoteLimit--;
                result.VoteDate = vote.VoteDate;
                _userInfoDal.Update(result);
                return new SuccessDataResult<Vote>();
            }
            else
            {
                result.VoteLimit = 2;
                _userInfoDal.Update(result);
            }

            
            return new SuccessResult();
        }


       
    }
}
