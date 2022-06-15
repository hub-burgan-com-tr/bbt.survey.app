using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserInfoManager : IUserInfoService
    {
        IUserInfoDal _userInfoDal;
        IVoteLimitDal _voteLimitDal;

        public UserInfoManager(IUserInfoDal userInfoDal,IVoteLimitDal voteLimitDal)
        {
            _userInfoDal = userInfoDal;
            _voteLimitDal = voteLimitDal;

        }


        public IResult Add(UserInfo userInfo)
        {
            var result =_userInfoDal.GetAll(x=>x.UserId == userInfo.UserId).ToList();
            var limit = _voteLimitDal.GetAll().FirstOrDefault().Limit;

            if (result.Count!=0)
            {
                //_userInfoDal.Update(userInfo);
                UserInfo resultData = _userInfoDal.GetAll(x => x.UserId == userInfo.UserId).FirstOrDefault();
                return new SuccessDataResult<UserInfo>(resultData, Messages.UserInfoAdd);
                
            }
            else
            {
                userInfo.VoteLimit = limit;
                _userInfoDal.Add(userInfo);
                UserInfo resultData = _userInfoDal.GetAll(x => x.UserId == userInfo.UserId).FirstOrDefault();


                return new SuccessDataResult<UserInfo>(resultData, Messages.UserInfoCheck);

            }
        }

        public IDataResult<UserInfo> GetAll(string sicilNo)
        {
            UserInfo userInfo = _userInfoDal.GetAll(x=>x.UserId==sicilNo).FirstOrDefault();
            return new SuccessDataResult<UserInfo>(userInfo,Messages.Listed);
        }

        public IResult Update(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
