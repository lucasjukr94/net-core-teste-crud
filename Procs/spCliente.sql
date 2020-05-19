USE [PBS_TESTE]
GO

/****** Object:  StoredProcedure [dbo].[spCliente]    Script Date: 19/05/2020 18:51:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[spCliente] @id int
as
begin
	set nocount on

	declare @l_id int = @id

	select top 1 * from CLIENTES where ID = @l_id
	select * from CLIENTE_ENDERECOS where ID_CLIENTE = @l_id
end
GO


