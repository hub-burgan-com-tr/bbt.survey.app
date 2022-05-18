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
    public class UserManager : IUserService
    {

        IUserDal _userDal;




        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;


        }



        public IDataResult<User> GetAll(string sicilNo)
        {

            

            try

            {
                User user = _userDal.GetAll(x => x.SicilNo == sicilNo).OrderByDescending(x => x.RecTime).FirstOrDefault();
                //List<User> result = _userDal.GetAll(x => x.SicilNo == sicilNo).OrderByDescending(x=>x.RecTime).FirstOrDefault();



                return new SuccessDataResult<User>(user, Messages.Listed);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                //return new ErrorDataResult<User>(Messages.CannotBeListed);
                return new ErrorDataResult<User>(e.Message);
            }




        }
    }
}
