-- create table unidade (
-- 	id_unidade serial primary key,
-- 	nome varchar(150) not null,
-- 	endereco varchar(200) not null,
-- 	status int not null default 1
-- );
select * from unidade

insert into unidade (nome,endereco) values ('Escola Central FIAP', 'Av Paulista, Nº 521')
insert into unidade (nome,endereco) values ('USJT', 'Av Taquiri, Nº 425')
insert into unidade (nome,endereco) values ('USP', 'Av Morumbi, Nº 1001')


-- create table professor (
-- 	id_professor serial primary key,
-- 	nome varchar(150) not null,
-- 	idade int not null,
-- 	data_nascimento date not null,
-- 	status int not null default 1,
-- 	id_unidade int not null,
-- 	constraint id_unidade_fkey foreign key(id_unidade)
-- 		references unidade (id_unidade)
-- );
select * from professor

insert into professor (nome,idade,data_nascimento,id_unidade) values ('Fernando', 32, '1987-09-20', 1)

-- create table aluno (
-- 	id_aluno serial primary key,
-- 	nome varchar(150) not null,
-- 	idade int not null,
-- 	data_nascimento date not null,
-- 	status int not null default 1,
-- 	id_unidade int not null,
-- 	constraint in_unidade_fkey foreign key (id_unidade)
-- 		references unidade (id_unidade)
-- );
select * from aluno

insert into aluno (nome,idade,data_nascimento,id_unidade) values ('Felipe', 23, '1997-09-20', 1)

-- create table professor_aluno(
-- 	id_professor int not null,
-- 	id_aluno int not null,
-- 	status int not null default 1,
-- 	constraint id_aluno_fkey foreign key (id_aluno)
-- 		references aluno (id_aluno),
-- 	constraint id_professor_fkey foreign key (id_professor)
-- 		references professor (id_professor),
-- 	primary key (id_professor, id_aluno, status)
-- );

select * from professor_aluno

insert into professor_aluno (id_professor,id_aluno) values (1 ,1)
