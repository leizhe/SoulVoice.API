using System;
using System.Collections.Generic;
using AutoMapper;
using SV.Application.ServiceContract;
using SV.Entity.Command;
using SV.Repository.Core.Command;
using SV.Repository.Core.Query;

namespace SV.Application.ServiceImp
{
    public class TestService : BaseService, ITestService
    {
        private readonly IUserQueryRepository _userQuery;
        private readonly IUserCommandRepository _userCommand;
        private readonly IMapper _mapper;
        public TestService(IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository,
            IMapper mapper
        )
        {
            _userQuery = userQueryRepository;
            _userCommand = userCommandRepository;
            _mapper = mapper;
        }


        public void QueryTest()
        {
            //findSingle
            var findSingleById = _userQuery.FindSingle(1);
            var findSingleByExp = _userQuery.FindSingle(p => p.Id == 1);
            var findSingleByExpOrder = _userQuery.FindSingle(p=>p.Id==1,new {name=true});

            //findAll
            var findAll = _userQuery.FindAll();
            var findAllByOrder = _userQuery.FindAll(new { name = true });

            //findByExp
            var findByExp = _userQuery.Find(p => p.Id == 1);
            var findByExpOrder = _userQuery.Find(p => p.Id == 1, new { name = true });


            //page
            var page = _userQuery.Page(1, 10, out var pageCount);
            var pagecount = pageCount;

            var pageByExpOrder = _userQuery.Page(1, 10, out var pageByExpOrderCount, user=>user.Id==1, new { Name = true });
            var pageByExpOrdercount = pageByExpOrderCount;


            //Count
            var count = _userQuery.Count();
            var countByExp = _userQuery.Count(p=>p.Id>0);

            //Exists
            var exists = _userQuery.Exists(p => p.Id == 1);
        }

        public void CommandTest()
        {
            ////Add
            ////----------------------------------------------
            //var user = new User() { Name = "test1", Password = "testPass", CreationTime = DateTime.UtcNow };
            //_userCommand.Add(user);
            //_userCommand.Commit();
            ////----------------------------------------------

            ////----------------------------------------------
            //var userLst = new List<User>()
            //{
            //    new User() {Name = "testlst1", Password = "testlstPass", CreationTime = DateTime.UtcNow}
            //};
            //_userCommand.AddRange(userLst);
            //_userCommand.Commit();
            ////----------------------------------------------

            //Update
            //----------------------------------------------
            var userUp1 = _userQuery.FindSingle(4);
            userUp1.Alipay = "123";
            userUp1.Name = "1234";
            var commandUser = _mapper.Map<User>(userUp1);
            _userCommand.Update(commandUser);
            _userCommand.Commit();
            //----------------------------------------------

            //----------------------------------------------
            _userCommand.Update(f=>f.Id==4,p=> new Entity.Command.User() { Name = "testUp1", Password = "testPassUp", CreationTime = DateTime.UtcNow });
            //----------------------------------------------



            _userCommand.Commit();
            
        }
    }
}