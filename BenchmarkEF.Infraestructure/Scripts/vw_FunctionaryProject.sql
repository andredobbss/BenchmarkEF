CREATE VIEW vw_FunctionaryProject
AS
SELECT 
    [p].[ProjectId],
	[f].[FunctionaryName],  
	[f].[Position], 
	[d].[DepartmentName], 
	[p].[ProjectName], 
	[p].[Description], 
	[p].[Budget], 
	[p].[StartDate],  
	[p].[UpdateDate],
	[p].[EndDate],	 
	[p].[Status] 
FROM 
	[Functionaries] AS [f]
INNER JOIN 
	[Departments] AS [d] ON [f].[DepartmentId] = [d].[DepartmentId]
INNER JOIN 
	[FunctionaryProject] AS [f0] ON [f].[FunctionaryId] = [f0].[FunctionaryId]
INNER JOIN 
	[Projects] AS [p] ON [f0].[ProjectId] = [p].[ProjectId]