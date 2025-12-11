CREATE TABLE[dbo].[ELMAH_Error]  
(  
  
    [ErrorId][uniqueidentifier] NOT NULL,  
  
    [Application][nvarchar](60) NOT NULL,  
  
    [Host][nvarchar](50) NOT NULL,  
  
    [Type][nvarchar](100) NOT NULL,  
  
    [Source][nvarchar](60) NOT NULL,  
  
    [Message][nvarchar](500) NOT NULL,  
  
    [User][nvarchar](50) NOT NULL,  
  
    [StatusCode][int] NOT NULL,  
  
    [TimeUtc][datetime] NOT NULL,  
  
    [Sequence][int] IDENTITY(1, 1) NOT NULL,  
  
    [AllXml][ntext] NOT NULL  
  
)  


/*-----------------------------------*/

Create PROCEDURE[dbo].[ELMAH_GetErrorsXml]  
  
(  
    @Application NVARCHAR(60),  
    @PageIndex INT = 0,  
    @PageSize INT = 15,  
    @TotalCount INT OUTPUT  
  
)  
  
AS  
  
SET NOCOUNT ON  
  
DECLARE @FirstTimeUTC DATETIME  
DECLARE @FirstSequence INT  
DECLARE @StartRow INT  
DECLARE @StartRowIndex INT  
SELECT  
  
@TotalCount = COUNT(1)  
  
FROM  
  
    [ELMAH_Error]  
  
WHERE  
  
    [Application] = @Application  
SET @StartRowIndex = @PageIndex * @PageSize + 1  
IF @StartRowIndex <= @TotalCount  
  
BEGIN  
  
SET ROWCOUNT @StartRowIndex  
  
SELECT  
  
@FirstTimeUTC = [TimeUtc],  
  
    @FirstSequence = [Sequence]  
  
FROM  
  
    [ELMAH_Error]  
  
WHERE  
  
    [Application] = @Application  
  
ORDER BY  
  
    [TimeUtc] DESC,  
    [Sequence] DESC  
  
END  
  
ELSE  
  
BEGIN  
  
SET @PageSize = 0  
  
END  
  
SET ROWCOUNT @PageSize  
  
SELECT  
  
errorId = [ErrorId],  
  
    application = [Application],  
    host = [Host],  
    type = [Type],  
    source = [Source],  
    message = [Message],  
    [user] = [User],  
    statusCode = [StatusCode],  
    time = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'  
  
FROM  
  
    [ELMAH_Error] error  
  
WHERE  
  
    [Application] = @Application  
  
AND  
  
    [TimeUtc] <= @FirstTimeUTC  
  
AND  
  
    [Sequence] <= @FirstSequence  
  
ORDER BY  
  
    [TimeUtc] DESC,  
  
    [Sequence] DESC  
  
FOR  
  
XML AUTO 


/*----------------------------------------------*/

Create PROCEDURE[dbo].[ELMAH_LogError]  
  
(  
  
    @ErrorId UNIQUEIDENTIFIER,    
    @Application NVARCHAR(60),    
    @Host NVARCHAR(30),    
    @Type NVARCHAR(100),  
    @Source NVARCHAR(60),    
    @Message NVARCHAR(500),  
    @User NVARCHAR(50),   
    @AllXml NTEXT,    
    @StatusCode INT,   
    @TimeUtc DATETIME  
  
)  
  
AS  
  
SET NOCOUNT ON  
  
INSERT  
  
INTO  
  
    [ELMAH_Error]
(  
  
    [ErrorId],   
    [Application],   
    [Host],  
    [Type],  
    [Source],  
    [Message],    
    [User],    
    [AllXml],    
    [StatusCode],    
    [TimeUtc]  
  
)  
  
VALUES  
  
    (  
  
    @ErrorId,  
    @Application,    
    @Host,    
    @Type,    
    @Source,   
    @Message,    
    @User,   
    @AllXml,   
    @StatusCode,   
    @TimeUtc   
)   