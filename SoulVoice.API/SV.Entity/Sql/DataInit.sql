insert into User(Name,Email,Money,Phone,Password,CreationTime,State) value('admin','Email',0,'Phone','123','2018-5-25',0);

insert into Role(Name,Memo) value('管理员','dada');
insert into Role(Name,Memo) value('会员','member');
insert into Role(Name,Memo) value('主播','anchor');

insert into UserRole(RoleId,UserId) value(1,1);


insert into Permission(Access,AccessValue) value(0,1);
insert into Permission(Access,AccessValue) value(1,1);


insert into RolePermission(PermissionId,RoleId) value(1,1);
insert into RolePermission(PermissionId,RoleId) value(2,1);