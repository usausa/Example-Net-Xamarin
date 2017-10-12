CREATE TABLE [Storage] (
    [StorageNo] [int] NOT NULL,
    [EntryUserId] [int] NOT NULL,
    [EntryAt] DATETIME2 NOT NULL,
    [InspectionUserId] [int] NULL,
    [InspectionAt] DATETIME2 NULL,
    CONSTRAINT [PK_Storage] PRIMARY KEY CLUSTERED ([StorageNo] ASC)
);
GO


CREATE TABLE [StorageDetail] (
    [StorageNo] [int] NOT NULL,
    [DetailNo] [int] NOT NULL,
    [ItemCode] [varchar](20) NOT NULL,
    [ItemName] [nvarchar](20) NOT NULL,
    [SalesPrice] [bigint] NOT NULL,
    [Qty] [bigint] NOT NULL,
 CONSTRAINT [PK_StorageDetail] PRIMARY KEY CLUSTERED ([StorageNo] ASC, [DetailNo] ASC)
);
GO


ALTER TABLE [StorageDetail]
WITH CHECK ADD CONSTRAINT [FK_StorageDetail_Storage]
FOREIGN KEY([StorageNo]) REFERENCES [Storage] ([StorageNo]) ON DELETE CASCADE;
GO


CREATE VIEW [StorageDetailSummaryView]
AS
SELECT
    StorageNo,
    COUNT(*) AS DetailCount,
    SUM(SalesPrice * Qty) AS TotalPrice,
    SUM(Qty) AS TotalQty
FROM
    StorageDetail
GROUP BY
    StorageNo;
GO


CREATE VIEW [StorageView]
AS
SELECT
    T.StorageNo,
    T.EntryUserId,
    T.EntryAt,
    T.InspectionUserId,
    T.InspectionAt,
    T1.DetailCount,
    T1.TotalPrice,
    T1.TotalQty
FROM
    Storage T LEFT OUTER JOIN StorageDetailSummaryView T1 ON T.StorageNo = T1.StorageNo;
GO
