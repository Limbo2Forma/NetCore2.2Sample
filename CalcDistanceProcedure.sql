DROP PROCEDURE dbo.CalculateDistanceTrip
GO
CREATE PROCEDURE CalculateDistanceTrip 
@Id BIGINT
AS
BEGIN
	SET NOCOUNT ON; 
	SELECT dbo.fnCalculateDistance(pickup_longitude,pickup_latitude,
	dropoff_longitude,dropoff_latitude) FROM dbo.TaxiSample WHERE Id = @Id
END
GO

