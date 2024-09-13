DROP DATABASE IF EXISTS library;

CREATE DATABASE library;
USE library;

CREATE TABLE author
(
	author_id int ,
    first_name varchar(50),
    last_name varchar(50),
    date_of_birth date,
    CONSTRAINT PK_Author PRIMARY KEY (author_id)
);

CREATE TABLE book
(
	book_id int,
    author_id int,
    book_title tinytext,
    publish_date date,
    CONSTRAINT PK_Book PRIMARY KEY (book_id),
    CONSTRAINT FK_Author FOREIGN KEY (author_id) REFERENCES author(author_id)
);