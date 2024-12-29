DROP TABLE IF EXISTS author;
DROP TABLE IF EXISTS book;
DROP VIEW IF EXISTS v_recent_books;


CREATE TABLE author
(
	author_id integer primary key,
    first_name varchar(50),
    last_name varchar(50),
    pseudonym varchar(50),
    date_of_birth year
);

CREATE TABLE book
(
	book_id integer primary key,
    author_id int,
    book_title tinytext,
    publish_year year,
    CONSTRAINT FK_Author FOREIGN KEY (author_id) REFERENCES author(author_id)
);

CREATE VIEW v_recent_books as
SELECT IIF(a.pseudonym IS NOT NULL, a.pseudonym, concat(a.first_name, ' ', a.last_name)) as author_name,
	   b.book_title,
       b.publish_year
FROM book b
LEFT OUTER JOIN author a
ON b.author_id = a.author_id
WHERE b.publish_year > 1899
ORDER BY publish_year;

INSERT INTO author
	(
    first_name,
    last_name,
    pseudonym,
    date_of_birth
    )
VALUES
	("Clide", "Lewis", "C.S. Lewis", "1963"),
    ("James", "Clear", null, "1986")
    ;

INSERT INTO book
	(
    book_title,
    publish_year,
    author_id
    )
VALUES
	("Atomic Habits", "2018", 2),
    ("The Abolition of Man", "1944", 1),
    ("A Grief Observed", "1964", 1),
    ("The Great Divorce", "1946", 1),
    ("Miracles", "1947", 1),
    ("The Screwtape Letters", "1942", 1),
    ("Mere Christianity", "1952", 1)
    ;