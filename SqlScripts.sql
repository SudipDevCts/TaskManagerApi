Create TABLE ParentTask_Table (
  Parent_ID    int IDENTITY(1,1) PRIMARY KEY not null, 
  Parent_Task  varchar(4000)
);

CREATE TABLE Task_Table (
  Task_ID int IDENTITY(1,1) PRIMARY KEY not null, 
  Task   varchar (4000), 
  Priority   INTEGER,
  Parent_ID INTEGER,
  Start_Date DateTime,
  End_Date DateTime,
  FOREIGN KEY(Parent_ID) REFERENCES ParentTask_Table(Parent_ID)
);

select * from dbo.ParentTask_Table

select * from dbo.Task_Table

