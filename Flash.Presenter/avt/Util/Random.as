package avt.Util {
	
	public class Random {
		
		static public function randomNumber(low:Number=0, high:Number=100):Number
		{
		  	return Math.floor(Math.random() * (1+high-low)) + low;
		}
		
	}
}