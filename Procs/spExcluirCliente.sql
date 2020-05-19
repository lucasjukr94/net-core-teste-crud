USE [PBS_TESTE]
GO

/****** Object:  StoredProcedure [dbo].[spExcluirCliente]    Script Date: 19/05/2020 18:52:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[spExcluirCliente] @id int
as
begin
	set nocount on
	
	declare @l_id int = @id

	delete from CLIENTE_ENDERECOS where ID_CLIENTE = @l_id
	delete from CLIENTES where ID = @l_id
end
GO


