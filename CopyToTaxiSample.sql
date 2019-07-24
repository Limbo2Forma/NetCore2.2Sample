CREATE PROCEDURE InsertToTaxiSample
AS
BEGIN
	INSERT INTO dbo.TaxiSample (
		medallion,
		hack_license,
		vendor_id,
		rate_code,
		store_and_fwd_flag,
		pickup_datetime,
		dropoff_datetime,
		passenger_count,
		trip_time_in_secs,
		trip_distance,
		pickup_longitude,
		pickup_latitude,
		dropoff_longitude,
		dropoff_latitude,
		payment_type,
		fare_amount,
		surcharge,
		mta_tax,
		tolls_amount,
		total_amount,
		tip_amount,
		tipped,
		tip_class
	)
	SELECT  
		medallion,			
		hack_license,        
		vendor_id,            
		rate_code,			 
		store_and_fwd_flag, 
		pickup_datetime,	
		dropoff_datetime,	
		passenger_count,  
		trip_time_in_secs,   
		trip_distance,       
		pickup_longitude,   
		pickup_latitude,     
		dropoff_longitude,    
		dropoff_latitude,    
		payment_type,	
		fare_amount,		
		surcharge,			
		mta_tax,			
		tolls_amount,		
		total_amount,			
		tip_amount,		
		tipped,				
		tip_class				
	FROM dbo.nyctaxi_sample

END;

