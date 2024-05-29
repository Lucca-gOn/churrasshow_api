SELECT TOP (1000) [IdUsuario]
      ,[Nome]
      ,[RG]
      ,[CPF]
      ,[Email]
      ,[Senha]
      ,[Foto]
      ,[CodRecupSenha]
      ,[IdTipoUsuario]
      ,[IdEndereco]
  FROM [ChurrasShow].[dbo].[Usuario]

 INSERT INTO TiposUsuario (IdTipoUsuario, TituloTipoUsuario)
VALUES (NEWID(), 'Administrador');
