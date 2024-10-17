USE library;

DROP VIEW IF EXISTS v_recent_books ;

CREATE VIEW v_recent_books as
SELECT a.pseudonym,
	   b.book_title,
       b.publish_year
FROM book b
LEFT OUTER JOIN author a
ON b.author_id = a.author_id
WHERE b.publish_year > 1899
ORDER BY publish_year;