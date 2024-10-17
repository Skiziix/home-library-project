DROP DATABASE IF EXISTS library;

CREATE DATABASE library;
USE library;

CREATE TABLE author
(
	author_id int NOT NULL AUTO_INCREMENT,
    first_name varchar(50),
    last_name varchar(50),
    pseudonym varchar(50),
    date_of_birth year,
    CONSTRAINT PK_Author PRIMARY KEY (author_id)
);

CREATE TABLE book
(
	book_id int NOT NULL AUTO_INCREMENT,
    author_id int,
    book_title tinytext,
    publish_year year,
    CONSTRAINT PK_Book PRIMARY KEY (book_id),
    CONSTRAINT FK_Author FOREIGN KEY (author_id) REFERENCES author(author_id)
);