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
        static readonly object obj = new object();

        public VoteManager(IVoteDal voteDal, IUserInfoDal userInfoDal)
        {
            _voteDal = voteDal;
            _userInfoDal = userInfoDal;
        }

        public IResult Add(Vote vote)
        {
            lock (obj)
            {
                vote.VoteDate = DateTime.Now.Date;
                vote.Date = DateTime.Now;
                var result = _userInfoDal.Get(x => x.UserId == vote.UserId);

                if (result.VoteDate == null || result.VoteDate.Date != vote.VoteDate.Date)
                {
                    result.VoteDate = vote.VoteDate;
                    result.VoteLimit = 0;
                    _voteDal.Add(vote);
                    _userInfoDal.Update(result);

                    return new SuccessResult(Messages.VoteSuccess);
                }
                else
                {
                    return new SuccessResult(Messages.VoteFailed);
                }
            }

        }

    }
}
