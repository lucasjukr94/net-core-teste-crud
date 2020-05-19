USE [PBS_TESTE]
GO

/****** Object:  StoredProcedure [dbo].[spCriarCliente]    Script Date: 19/05/2020 18:51:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[spCriarCliente] 
	@NOME varchar(200)
	, @DT_NASCIMENTO datetime
	, @STATUS tinyint
	, @JSON_ENDERECOS varchar(max)
as
begin
	set nocount on

	declare @l_NOME varchar(200) = @NOME
		, @l_DT_NASCIMENTO datetime = @DT_NASCIMENTO
		, @l_STATUS tinyint = @STATUS
		, @l_DAT_INCLUSAO datetime = getdate()

	insert into CLIENTES(NOME,DT_NASCIMENTO,STATUS,DAT_INCLUSAO) values(@l_NOME,@l_DT_NASCIMENTO,@l_STATUS,@l_DAT_INCLUSAO)

	declare @l_ID_CLIENTE int = scope_identity()

	insert into CLIENTE_ENDERECOS(
		ID_CLIENTE
		, LOGRADOURO
		, CEP
		, UF
		, CIDADE
		, BAIRRO
		, STATUS
		, DAT_INCLUSAO)
	(
		select 
			@l_ID_CLIENTE
			,*
			,getdate() 
		from openjson(@JSON_ENDERECOS) with(
			LOGRADOURO nvarchar(100)
			, CEP nvarchar(8)
			, UF nvarchar(2)
			, CIDADE nvarchar(100)
			, BAIRRO nvarchar(60)
			, STATUS tinyint
		)
	)

	select top 1 * from CLIENTES where ID = @l_ID_CLIENTE
	select * from CLIENTE_ENDERECOS where ID_CLIENTE = @l_ID_CLIENTE
end
GO


