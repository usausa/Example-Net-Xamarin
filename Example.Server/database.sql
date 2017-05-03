CREATE TABLE IF NOT EXISTS storage (
    storage_no integer NOT NULL,
    entry_user_id integer NOT NULL,
    entry_at timestamp NOT NULL,
    inspection_user_id integer NULL,
    inspection_at timestamp NULL,
    PRIMARY KEY (storage_no)
);

CREATE TABLE IF NOT EXISTS storage_detail (
    storage_no integer NOT NULL,
    detail_no integer NOT NULL,
    item_code text NOT NULL,
    item_name text NOT NULL,
    sales_price integer NOT NULL,
    amount integer NOT NULL,
    PRIMARY KEY (storage_no, detail_no)
);

CREATE VIEW IF NOT EXISTS storage_detail_summary_view
AS
SELECT
    storage_no,
    COUNT(*) AS detail_count,
    SUM(sales_price * amount) AS total_price,
    SUM(amount) AS total_amount
FROM
    storage_detail
GROUP BY
    storage_no;

CREATE VIEW IF NOT EXISTS storage_view
AS
SELECT
    t.storage_no,
    t.entry_user_id,
    t.entry_at,
    t.inspection_user_id,
    t.inspection_at,
    t1.detail_count,
    t1.total_price,
    t1.total_amount
FROM
    storage t LEFT OUTER JOIN storage_detail_summary_view t1 ON t.storage_no = t1.storage_no;
