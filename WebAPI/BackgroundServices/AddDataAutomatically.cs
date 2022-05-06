using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.BackgroundServices
{
    public class AddDataAutomatically : BackgroundService,IDisposable
    {
        private readonly ILogger<AddDataAutomatically> _logger;
        IUserTestDal _userTestDal;
        IVoteLimitDal _voteLimitDal;
        IUserInfoDal _userInfoDal;
        public AddDataAutomatically(ILogger<AddDataAutomatically> logger, IUserTestDal userTestDal, IVoteLimitDal voteLimitDal, IUserInfoDal userInfoDal)
        {
            _logger = logger;
            _userTestDal = userTestDal;
            _voteLimitDal = voteLimitDal;
            _userInfoDal = userInfoDal;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime dateTime = DateTime.Now;
                //Console.WriteLine($"Saat bilgisi {DateTime.Now}");
                //Console.WriteLine($"Saat bilgisi {dateTime}");
                //_logger.LogInformation("Bakcground servis çalışıyor", dateTime);

                if (dateTime.Hour == 24&& IfUserHasNoDataAddAutomatically().Success)
                {
                    Console.WriteLine("Eksik bilgiler eklendi");
                }


                await Task.Delay(1000);
            }
            
        }

        private async Task GetDatetime()
        {
           
            
            await Task.Delay(1000);
        }

        private IResult IfUserHasNoDataAddAutomatically()
        {
            List<UserInfo> result = _userInfoDal.GetAll().ToList();
            List<UserTest> voteResult = _userTestDal.GetAll(x => x.VoteDate.Month == DateTime.Now.Month && x.VoteDate.Day == DateTime.Now.Day && x.VoteDate.Year == DateTime.Now.Year).ToList();
            var limit = _voteLimitDal.GetAll().FirstOrDefault().Limit;
            List<UserInfo> users = new List<UserInfo>();


            //Task Scheduler araştır.Worker sınıfı yaz.
            //var data = result.Select(x => x.Id).Except(voteResult.Select(y => y.UserId)).ToList();

            var data = result.Select(x => new { x.Id, x.Section, x.Unit, x.Department }).ToList();
            var votedata = voteResult.Select(x => new { x.UserId }).ToList();

            //var sq= from res in result join res2 in voteResult on res.Id equals res2.UserId where(res.Id!=res2.UserId) select new { res.Id, res.Section, res.Unit, res.Department };

            var dd = from res in result from res2 in votedata where res.Id != res2.UserId select new { res.Id, res.Section, res.Unit, res.Department };



            IList newResult = data.Select(x => x.Id).Except(voteResult.Select(y => y.UserId)).ToList();

            //var ddd=data.Where(x=> !voteResult.Contains(voteResult[x.Id])).ToList();


            if (newResult.Count != 0)
            {
                foreach (var item in dd)
                {
                    UserTest user = new UserTest { VoteLimit = limit, VoteDate = DateTime.Now, Vote = 0, Date = DateTime.Now };
                    do
                    {
                        user.Id = 0;
                        user.UserId = item.Id;
                        user.Department = item.Department;
                        user.Section = item.Section;
                        user.Unit = item.Unit;
                        user.VoteLimit -= 1;
                        _userTestDal.Add(user);

                    } while (user.VoteLimit != 0);

                }
                newResult.Clear();
            }
            return new SuccessResult();
        }





        private IResult ListUserAndAddMissingData()
        {
            var limit = _voteLimitDal.GetAll().FirstOrDefault().Limit;
            //List<UserTest> resultUserData = _userTestDal.GetAll().Where(x => x.VoteLimit < limit && x.VoteLimit != 0).ToList();
            //List<UserTest> resultUserData = _userTestDal.GetAll(x => x.VoteLimit < limit && x.VoteLimit != 0).ToList();
            List<UserTest> users = new List<UserTest>();


            List<UserTest> resultUserData = _userTestDal.GetAll().ToList();



            resultUserData.ForEach(x => users.Add(x));
            if (users.FindLast(x => x.VoteLimit != 0).VoteLimit < limit && users.Count != 0)
            {
                foreach (var item in users)
                {
                    //if (item.VoteLimit<limit&&item.VoteLimit!=0)
                    //{
                    //    item.VoteLimit -=item.VoteLimit;
                    //    item.Date = DateTime.Now;
                    //}


                    do
                    {
                        item.VoteLimit -= 1;
                        item.Date = DateTime.Now;
                        item.Vote = 0;
                        item.Id = 0;


                        _userTestDal.Add(item);


                    } while (item.VoteLimit != 0);

                }
                users.Clear();
            }
            else
            {
                return new ErrorResult();
            }

            return new SuccessResult();

        }

    }
}
