using Business.Abstract;
using Business.Constants;
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
            vote.VoteDate = DateTime.Now.Date;
            vote.Date = DateTime.Now;
            var result= _userInfoDal.Get(x=>x.UserId==vote.UserId);
            if (result.VoteLimit>0&&result.VoteDate<=vote.VoteDate)
            {
                //vote.UserId = null;
                _voteDal.Add(vote);
                result.VoteLimit--;
                result.VoteDate = vote.VoteDate;
                _userInfoDal.Update(result);
                return new SuccessResult(Messages.VoteSuccess);
            }
            else if(result.VoteDate!=vote.VoteDate)
            {
                result.VoteLimit = 1;
                //vote.UserId=null;
                _voteDal.Add(vote);
                _userInfoDal.Update(result);
                return new SuccessResult(Messages.VoteSuccess);
            }
            else
            {
                return new ErrorResult(Messages.VoteFailed);
            }
        }

    }
}
