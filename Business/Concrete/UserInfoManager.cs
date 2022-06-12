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

        public UserInfoManager(IUserInfoDal userInfoDal)
        {
            _userInfoDal = userInfoDal;

        }


        public IResult Add(UserInfo userInfo)
        {
            var result =_userInfoDal.GetAll(x=>x.UserId == userInfo.UserId).ToList();
            
            if (result.Count!=0)
            {
                //_userInfoDal.Update(userInfo);
                UserInfo resultData = _userInfoDal.GetAll(x => x.UserId == userInfo.UserId).FirstOrDefault();
                return new SuccessDataResult<UserInfo>(resultData, Messages.UserInfoAdd);
                
            }
            else
            {
                userInfo.VoteLimit = 2;
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
    }
}
