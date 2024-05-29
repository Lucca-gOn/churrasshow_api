SELECT TOP (1000) [IdStatusEvento]
      ,[Status]
  FROM [ChurrasShow].[dbo].[StatusEvento]

  INSERT INTO [ChurrasShow].[dbo].[StatusEvento] (IdStatusEvento, Status)
VALUES (NEWID(), 'Ativo'),
       (NEWID(), 'Inativo');