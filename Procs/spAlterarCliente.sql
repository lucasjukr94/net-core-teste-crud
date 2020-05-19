USE [PBS_TESTE]
GO

/****** Object:  StoredProcedure [dbo].[spAlterarCliente]    Script Date: 19/05/2020 18:51:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create proc [dbo].[spAlterarCliente] 
	@ID int
	, @NOME varchar(200) = null
	, @DT_NASCIMENTO datetime = null
	, @STATUS tinyint = null
	, @JSON_ENDERECOS varchar(max) = null
as
begin
	set nocount on

	declare @l_ID int = @ID
			, @l_NOME varchar(200) = @NOME
			, @l_DT_NASCIMENTO datetime = @DT_NASCIMENTO
			, @l_STATUS tinyint = @STATUS

	update	CLIENTES 
	set		NOME = isnull(@l_NOME,NOME)
			, DT_NASCIMENTO = isnull(@l_DT_NASCIMENTO,DT_NASCIMENTO)
			, STATUS = isnull(@l_STATUS,STATUS) 
	where	ID = @l_ID 

	declare @enderecosTable table (
		ID int,
		LOGRADOURO nvarchar(100),
		CEP nvarchar(8),
		UF nvarchar(2),
		CIDADE nvarchar(100),
		BAIRRO nvarchar(60),
		STATUS tinyint
	)

	insert into @enderecosTable(
		ID
		, LOGRADOURO
		, CEP
		, UF
		, CIDADE
		, BAIRRO
		, STATUS )
	(
		select 
			*
		from openjson(@JSON_ENDERECOS) with(
			ID int,
			LOGRADOURO nvarchar(100),
			CEP nvarchar(8),
			UF nvarchar(2),
			CIDADE nvarchar(100),
			BAIRRO nvarchar(60),
			STATUS tinyint
		)
	)

	update t1
	set 
		t1.LOGRADOURO = isnull(t2.LOGRADOURO, t1.LOGRADOURO)
		, t1.CEP = isnull(t2.CEP, t1.CEP)
		, t1.UF = isnull(t2.UF, t1.UF)
		, t1.CIDADE = isnull(t2.CIDADE, t1.CIDADE)
		, t1.BAIRRO = isnull(t2.BAIRRO, t1.BAIRRO)
		, t1.STATUS = isnull(t2.STATUS, t1.STATUS)
	from
		CLIENTE_ENDERECOS as t1
		inner join @enderecosTable as t2 on t2.ID = t1.ID and t1.ID_CLIENTE = @l_ID
end
GO


