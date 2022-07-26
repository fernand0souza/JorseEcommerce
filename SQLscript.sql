DROP DATABASE JoseEcommerce
GO

CREATE DATABASE JoseEcommerce
GO

USE JoseEcommerce
GO

DROP TABLE Usuarios
GO 

CREATE TABLE Usuarios(
Id [int] IDENTITY (1,1) NOT NULL,
Documento [varchar](18) NOT NULL,
Nome [varchar](100) NOT NULL,
Telefone [varchar](11) NOT NULL,
Email [varchar](100) NOT NULL,
TipoPessoa [int] NOT NULL CONSTRAINT TipoPessoa DEFAULT (0), --PESSOA FISICA, PESSOA JURIDICA
FlagAnunciante [int] NOT NULL CONSTRAINT FlagAnunciante DEFAULT (0),
DataCadastro [datetime] NOT NULL DEFAULT GETDATE(),
Flagsituacao [int] NOT NULL CONSTRAINT Flagsituacao DEFAULT (0)
);



DROP TABLE Logs
GO 

CREATE TABLE Logs (
  [Log_id] [int] IDENTITY (1, 1) NOT NULL,
  [Log_NameProced] [varchar] (50) NOT NULL,
  [Log_txParamentros] [varchar] (MAX) NOT NULL,
  [Log_data] [datetime] NULL CONSTRAINT [DF_log_requisicao_log_dtentrada] DEFAULT (GETDATE())
);


DROP PROCEDURE [dbo].[sp_p_GravarUser]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_p_GravarUser] (
	@Nome				        VARCHAR(100) = '',
	@Telefone	                VARCHAR(11) = '',
    @Email				        VARCHAR(100) = '',
    @Documento                  VARCHAR(18) = '',                 
    @TipoPessoa                 INT = 0  
 )
AS
BEGIN 
    SET NOCOUNT ON;
    ---------
    -- LOG --
    ---------
	-- #################################################################################
    DECLARE @Log_txParamentros varchar(MAX) = NULL
    SET @Log_txParamentros = CONCAT (
        'EXEC sp_p_GravarUser',
		', @Nome=',   CONVERT(VARCHAR, ISNULL(@Nome, '')),
		', @Telefone=',   CONVERT(VARCHAR, ISNULL(@Telefone, '')),
	    ', @Email=',  CONVERT(VARCHAR, ISNULL(@Email, '')),
        ', @Documento=', CONVERT(VARCHAR, ISNULL(@Documento, '')),
        ', @TipoPessoa=', CONVERT(VARCHAR, ISNULL(@TipoPessoa, ''))
	)
		
	INSERT INTO [dbo].[Logs] ([Log_NameProced], [Log_txParamentros], [Log_data])
	VALUES ('sp_p_GravarUser', @Log_txParamentros,GETDATE())
	-- #################################################################################

	-------------------------
	----   VARIAVEIS --------
	-------------------------
	DECLARE @MENSAGEM VARCHAR(MAX)=''
	DECLARE @ERRO INT = 0
	DECLARE @EXISTE INT = 0

	--------------------------
	----- TRATAMENTOS --------
	--------------------------

	--CONSULTA DOCUMENTO PERTENCE AO USUARIO
	SELECT @EXISTE = COUNT(*) FROM [JoseEcommerce].[dbo].[Usuarios] us
	WHERE us.Documento = @Documento

    IF(@EXISTE > 0)
		SET @ERRO = 1


	IF(@EXISTE > 0 AND @ERRO > 0)
	BEGIN
		SET @MENSAGEM = IIF(@TipoPessoa = 0, 'CPF', 'CNPJ')+' JÁ PERTENCE AO USUARIO'
		SET @ERRO += 1 
	END
	--

	IF(@ERRO = 0)
	BEGIN 
		INSERT INTO [dbo].[Usuarios]
					([Documento]
					,[Nome]
					,[Telefone]
					,[Email]
					,[TipoPessoa]
					,[FlagAnunciante]
					,[DataCadastro]
					,[Flagsituacao])
				VALUES
					(@Documento,
					@Nome,
					@Telefone,
					@Email,
					@TipoPessoa,
					1,
					GETDATE(),
					1)

		SET @MENSAGEM = 'CADASTRO REALIZADO COM SUCESSO !'
		SET @ERRO = 0
	
	END 

	SELECT @MENSAGEM AS 'StatusMessage',
		   @ERRO	 AS 'StatusCode'

END




GO
