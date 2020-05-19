USE [PBS_TESTE]
GO

/****** Object:  StoredProcedure [dbo].[spListarClientes]    Script Date: 19/05/2020 18:52:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[spListarClientes] 
as
begin
	select * from CLIENTES
	select * from CLIENTE_ENDERECOS
end
GO


