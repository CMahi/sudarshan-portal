function validateSplChar(str,displayName)
{ 
    try
    { 
        var spchar, getChar, SpecialChar; 
        spchar="`~&|";
        getChar='Empty';
        SpecialChar='No';
        var spchars ="`~&|"; 
        for(var i=0; i<str.length; i++)
        {
            for(var j=0; j<spchar.length; j++)
            { 
                if(str.charAt(i)== spchar.charAt(j))
                { 
                    SpecialChar='Yes';
                    break;
                }
                else
                {
                    if (str.charAt(i)!=' ')
                    getChar='Normal';
                }
            } 
        }
        if(SpecialChar=='Yes')
        {
          alert("ValidationErr- " + displayName + " field contains special characters.");
          return false;
        }
        return true;        
    }
    catch(Exc){return false;}
}
function checkDecimal(val, id) {
    var ch;
    var decimalPointCount = 0;
    for (var index = 0; index < val.length; index++) {
        ch = val.charAt(index);
        if (!(((ch >= "0") && (ch <= "9")) || (ch == "."))) {
            alert("ValidationErr- Please enter a valid Numeric value!");
            document.getElementById(id).value = "";
            return false;
        }
        else {
            if (ch == ".") {
                var ch1 = val.substring(val.indexOf(".") + 1, val.length);
                // alert(ch.substring(ch.indexOf("."),ch.length));
                if (ch1.length >= 3)
                    if (/\d*\.\d\d$/.test(ch1))
                        return true;
                    else {
                        alert("Only two decimal places allowed");
                        document.getElementById(id).value = "";
                        return false;
                    }
            }


        }
    }
}
function isEmpty(value)
{
   return (trimAll(value)=="");
}
function stripNonDecimal(value) 
{
      var result = "";
      var ch;              
      value += ""; 
      for (var i=0; i<value.length; i++) 
      {
            ch = value.charAt(i);
            if (((ch >= "0") && (ch <= "9")) || (ch == "."))
              result += ch;
      }
      return result;
}
function parseDecimal(value) 
{
   return parseFloat(stripNonDecimal(value));
}
function validateBoolean( booleanVal )
{
   booleanVal = getElementValue( booleanVal );
   if ( booleanVal.toLowerCase() == 'true' || booleanVal.toLowerCase() == 'false' ) 
      return true;
   return false;
}
function validateLong( longVal )
{

   longVal = getElementValue( longVal );
   var index = longVal.lastIndexOf('.');
   if ( index >= 0 ) {
      return false;
   }
   else {
      index = longVal.lastIndexOf('e');
      if ( index >= 0 ) {
          return false;
      }

      index = longVal.lastIndexOf('E');

      if ( index >= 0 ) {
          return false;
      }
   }
	if ( isInteger(longVal)){
		return true;

   }else {
        return false;
   }
}
function isDigit(ch)
{
	return ((ch >= "0") && (ch <= "9"));	
}
function isInteger(integer)
{
	for(var i = 0; i <integer.length; i++)
	{
	      var ch = integer.charAt(i);
	      if(!isDigit(ch) && !(ch == "-")) 
	          return false;
    }
    return true;
}
function validateInteger(value,displayName)
{  
   if(!validateNumber(value,displayName))
         return false;
   if(!isInteger(value))
   {
       alert("ValidationErr- Please enter a valid integer number for \""+displayName+"\" field!");
       return false;
   }
   return true;
}
function isNumber(number)
{
  	var ch;
	var isNumber = true;
	var valid_chars = "-0123456789."; 
	
	for(var i=0; i<number.length && isNumber == true ;i++) 
	{
		ch = number.charAt(i);
		if(valid_chars.indexOf(ch) == -1)
			 isNumber = false;
	}
	if(number.indexOf(".") != number.lastIndexOf("."))
		 isNumber = false;
	if(number.indexOf("-") != -1)
    {
        if((number.indexOf("-") != 0) || (number.indexOf("-") != number.lastIndexOf("-")))
             isNumber = false;
    }
	return isNumber;
}
function validateNumber(value,displayName)
{
	if (trimAll(value) == "")
	{ 
	      alert("ValidationErr- Please enter a valid number for \""+displayName+"\" field!");
          return false;
    }
	if(!isNumber(value))
	{
	     alert("ValidationErr- Not a valid number! Please enter a valid number for \""+displayName+"\" field!");
	     return false;
    }
     return true;	 
}
function validateDecimal(value,displayName)
{
	if (trimAll(value) == "")
	{ 
	     alert("ValidationErr- Please enter a valid decimal value for \""+displayName+"\" field!");
          return false;
    }	
    if(!validateNumber(value,displayName))
         return false;
	var decimal = parseFloat(value);
	decimal = ''+decimal;
	if(decimal.length != value.length)
	{
		 alert("ValidationErr- Please enter a valid float value for \""+displayName+"\" field!");
	     return false;
    }
    else 
    {
        if(isNaN(parseFloat(value)))
        {
	 	    alert("ValidationErr- Please enter a valid float value for \""+displayName+"\" field!");
		    return false;
        }
        else
		    return true;
   }
}
function validateFloat(value,displayName)
{
      if(!validateNumber(value,displayName))
         return false;
	  var ch;              
	  var decimalPointCount = 0;	
	  for (var index=0; index<value.length; index++) 
	  {
	        ch = value.charAt(index);
	        if (!(((ch >= "0") && (ch <= "9")) || (ch == ".") || (ch == "-")))
	        { 
	            alert("ValidationErr- Please enter a valid float value for \""+displayName+"\" field!");
	            return false ;
	        }
	        else 
	        {
	            if(ch == ".") 
	            {
	    	        if(++decimalPointCount > 1) 
	    	        {
	    		        alert("ValidationErr- Please enter a valid float value for \""+displayName+"\" field!");
	    		        return false;
	    		    }
	    	    }
	        }
	  }
	  return true;
}
function validateCurrency(value,displayName) 
{
     if (value == "")
     { 
	     alert("ValidationErr- Please enter a valid currency value for \""+displayName+"\" field!");
         return false;
     }
	 var valuedec = parseDecimal(value);
     if (isNaN(valuedec)) 
     {
             alert("ValidationErr- "+value+" is not a valid currency ");
             return false;
     }
     else
     {
     	var decimalPlace = value.indexOf(".");
	    if( (decimalPlace == -1) )
	    {
	         alert("ValidationErr- Currency format is incorrect!\n Currency should be in ###.## format. ");
	         return false;
        }
	    else	
	        return true;
	}
}
function validateURL(value,displayName)
{
	
	if (trimAll(value)=="")
	{ 
	      alert("ValidationErr- Please enter a valid url for \""+displayName+"\" field!");
          return false;
    }
	
   	var http = value.substring(0,7);
	var https = value.substring(0,8);
	var ftp = value.substring(0,6);
	
	if((http == "http://") || (ftp == "ftp://") || (https == "https://"))
		return true;
	else
	{
	      alert("ValidationErr- "+value + " is not a valid URL.\n\n");
	      return false;
	}	
}
function validateCreditCard(value,displayName)
{
    var type="Visa";
    if((type =='Visa') || (type =='MasterCard'))
	  return validateCreditCardDefault(value,displayName);
    else if (type=='Amx')
	  return validateCreditCardAmx(value,displayName);
    else
	  return validateCreditCardDefault(value,displayName);
}
function validateCreditCardDefault(value,displayName)
{
	if (value == "")
	{ 
	    alert("ValidationErr- Please enter a valid credit card number for \""+displayName+"\" field!");
        return false;
    }
	if(value.length != 19)
	{
		alert("ValidationErr- Please enter Credit Card Number in ####-####-####-#### format.\n");
		return false; 
	}
	if( !isInteger(value.substring(0,4)) || !isInteger(value.substring(5,9)) || !isInteger(value.substring(10,14)) || !isInteger(value.substring(15,19)))
	{
	     alert("ValidationErr- Not a valid Credit Card Number, Please enter a valid credit card number!.\n");
		return false;
	}	
	if( isInteger(value.substring(0,4)) && isInteger(value.substring(5,9)) && isInteger(value.substring(10,14)) && isInteger(value.substring(15,19)))
	       return true;	
	return false;
}	
function validateCreditCardAmx(value,displayName)
{
	if (value == "")
	{ 
	      alert("ValidationErr- Please enter a valid credit card number for \""+displayName+"\" field!");
          return false;
    }
	if(value.length != 18)
	{
		alert("ValidationErr- Please enter Credit Card Number in ####-####-####-### format.\n");
		return false; 
	}
	if( !isInteger(value.substring(0,4)) || !isInteger(value.substring(5,9)) || !isInteger(value.substring(10,14)) || !isInteger(value.substring(15,18)))
	{
	    alert("ValidationErr- Not a valid Credit Card Number, Please enter a valid credit card number!.\n");
		return false;
	}	
	if( isInteger(value.substring(0,4)) && isInteger(value.substring(5,9)) && isInteger(value.substring(10,14)) && isInteger(value.substring(15,18)))
       return true;	
	return false;
}
function validatePostalCode(value,displayName)
{
  if (trimAll(value) == "")
  { 
	  alert("ValidationErr- Please enter a valid postal code for \""+displayName+"\" field!");
      return false;
  }
  if ((value.length >= 5) || (value.length == 5)) 
  {
	  var zipcode = parseInt(value.substring(0,5));
	  var zipcodestr = ''+zipcode;
      if( isNaN(parseInt(value.substring(0,5))) || (zipcodestr.length != 5))
      { 
	    	alert("ValidationErr- Not a valid Postal Code!\n First 5 digits should be number. ");
		    return false; 
	  }
	  else
	  { 
	      if(value.length > 5)
	      {  
		      if(isNaN(parseInt(value.indexOf("-"))) || (value.indexOf("-") != 5))
		      {
		            alert("ValidationErr- Not a valid Postal Code!\n Postal code ahould be in #####-####");
		            return false;
		      }
		      else 
		      {
		            if(!isNaN(parseInt(value.indexOf("-"))) && (value.indexOf("-") == 5))
		            {
		                var postalcode = value.substring(value.indexOf("-")+1,value.length);
		                var lastfour = parseInt(postalcode);
		                lastfour = ''+lastfour;
        		        if(lastfour.length != 4)
		                {
		                   alert("ValidationErr- Not a valid Postal Code!\n Postal code ahould be in #####-####");
		                   return false;
		                }
		                else 
		                {
		                      if(isNaN(postalcode))
		                      {
		                          alert("ValidationErr- Not a valid Postal Code!\n Last four digit should be a number.");
		                          return false; 
		                      }
		                      else
			                    return true; 
			            }    
			       }
			       if ((value.length != 5) && (value.length != 10)) 
                   {
                         alert(value + "  is not a valid US Zip Code.\n\n");
                         return false;
                   } 
                   else 
                    return true;
			}
	     }
	  }  
   }
}
function validatePhoneNumber(value,displayName)
{
	var i;
	var ch;
	if (trimAll(value) == "")
	{ 
	    alert("ValidationErr- Please enter a valid phone number for \""+displayName+"\" field!");
        return false;
    }
	if ((value.length != 0) && (value.length < 10))
	{
		alert("ValidationErr- Please enter exactly 10 digits in \"" + displayName + "\".\n Phone number should be in ###-###-#### format!\n");
		return false;
	}
	if (value.length > 12)
	{
		alert("ValidationErr- Please enter exactly 10 digits in \"" + displayName + "\".\n Phone number should be in ###-###-#### format!\n");
		return false;
	}
	for (i = 0; i < value.length; i++)
	{
	     var ch = value.charAt(i);
	        if(((i == 3) || (i == 7)) && ((ch != '-') && (ch !=' ')))
	        { 
		        alert("ValidationErr- Please enter only digits in ###-###-#### format!\n"+ch);
			    return false;
			}
			else 
			    if(((i != 3) && (i != 7)) &&  !isDigit(ch))
			    {
			        alert("ValidationErr- Please enter only digits in ###-###-#### format!\n");
			        return false;
		        }
	}
   /*	if (value.length == 12)
   	{
		if(value.charAt(0) < '2' )
		{
			alert("ValidationErr- The first digit of the area code in \"" + displayName + "\" cannot be a '1' or '0'");
          	return false;
		}
		else 
		  if(value.substring(0,3) == "900" )
		  {
			alert("ValidationErr- The area code in \"" + displayName + "\" cannot be '900'");
			return false;
		  }
		  else 
		    if(value.substring(0,3) == "911" )
		    {
			    alert("ValidationErr- The area code in \"" + displayName + "\" cannot be '911'");
			    return false;
		    }
		    else		
        	    return true;
    }*/
	return false;
}
function validateSSN(value,displayName) 
{
        
	if (trimAll(value) == "")
	{ 
	    alert("ValidationErr- Please enter a valid social security number for \""+displayName+"\" field!");
	    return false;
	}
	var matchArr = value.match(/^(\d{3})-?\d{2}-?\d{4}$/);
    var numDashes = value.split('-').length - 1;
    if (matchArr == null || numDashes == 1) 
    {
      alert("ValidationErr- "+value+' is invalid social security number. SSN must have nine digits.\nPlease enter social security number in NNN-NN-NNNN format.');
      return false;
    }
    else 
      if (parseInt(matchArr[1],10)==0) 
      {
          alert("ValidationErr- "+value+" is invalid social security number.\nSocial security number can't start with 000.");
          return false;    
      }
      else 
        return true;
}
function validate_Email(value,displayName) 
{
  var NO_SPACE_ERROR = "Email address should not have spaces.\n";
  var EMAIL_ALERT = "{0} is not a valid email address.\nEmail address should be in name@domain.com format.\n";
  if (trimAll(value) == "")
  { 
	  alert("ValidationErr- Please enter a valid email address for \""+displayName+"\" field!");
      return false;
  }

  value += "";
  var indexOfAtSign = value.indexOf("@");
  var indexOfPeriod = value.lastIndexOf('.');
  var indexOfFirst_last = value.indexOf(".");
  if( indexOfPeriod != indexOfFirst_last)
  {
     alert("ValidationErr- Email Address is wrong! "+NO_SPACE_ERROR);
     return false;
  
  }
  else 
     if (value.indexOf(" ") >= 0) 
     {
        alert("ValidationErr- Email Address is wrong! Email should be in name@domain.com format.");
        return false;
     } 
     else 
          if (indexOfAtSign < 1) 
          {
               alert("ValidationErr- Email Address is wrong! Email should be in name@domain.com format.");
               return false;
          } 
          else 
             if (value.indexOf("@", indexOfAtSign+1) >= 0) 
             {
                alert(EMAIL_ALERT.replace("{0}", label));
                return false;
             } 
             else 
                if ((indexOfPeriod-indexOfAtSign) < 2) 
                {
                       alert(EMAIL_ALERT.replace("{0}", value));
                       return false;
                       
                } 
                else 
                   if ((value.length - indexOfPeriod) < 3) 
                   {
                       alert(EMAIL_ALERT.replace("{0}", value));
                       return false;
                   } 
                   else 
                       return true;
    
}
function validateEmail(emailStr,displayName)
{
   var checkTLD = 1;
   var knownDomsPat = /^(com|net|org|edu|int|mil|gov|arpa|biz|aero|name|coop|info|pro|museum)$/;
   var emailPat = /^(.+)@(.+)$/;
   var specialChars = "\\(\\)><@,;:\\\\\\\"\\.\\[\\]";
   var validChars = "\[^\\s" + specialChars + "\]";
   var quotedUser = "(\"[^\"]*\")";
   var ipDomainPat = /^\[(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})\]$/;
   var atom=validChars + '+';
   var word = "(" + atom + "|" + quotedUser + ")";
   var userPat = new RegExp("^" + word + "(\\." + word + ")*$");
   var domainPat = new RegExp("^" + atom + "(\\." + atom +")*$");
   var matchArray = emailStr.match(emailPat);
   if (matchArray == null)
   {
      alert("ValidationErr- Email address seems incorrect in the field "+displayName+" (check @ and .'s)!");
      return false;
   }
  var user=matchArray[1];
  var domain=matchArray[2];
  for (i = 0; i < user.length; i++)
  {
        if (user.charCodeAt(i) > 127)
        {
              alert("ValidationErr- Ths username contains invalid characters in the field "+displayName+"!");
              return false;
        }
  }
  for (i = 0; i < domain.length; i++)
  {
        if (domain.charCodeAt(i) > 127)
        {
              alert("ValidationErr- Ths domain name contains invalid characters in the field "+displayName+"!");
              return false;
        }
  }
  if (user.match(userPat) == null)
  {
      alert("ValidationErr- The username doesn't seem to be valid in the field "+displayName+"!");
      return false;
  }
  var IPArray = domain.match(ipDomainPat);
  if (IPArray != null)
  {
    for (var i = 1; i <= 4; i++)
    {
          if (IPArray[i] > 255)
          {
            alert("ValidationErr- Destination IP address is invalid in the field "+displayName+"!");
            return false;
          }
    }
    return true;
  }
  var atomPat = new RegExp("^" + atom + "$");
  var domArr = domain.split(".");
  var len = domArr.length;
  for (i = 0; i < len; i++)
  {
        if (domArr[i].search(atomPat) == -1)
        {
              alert("ValidationErr- The domain name does not seem to be valid in the field "+displayName+"!");
              return false;
        }
  }
  if (checkTLD && domArr[domArr.length - 1].length != 2 && domArr[domArr.length - 1].search(knownDomsPat) == -1)
  {
        alert("ValidationErr- The address must end in a well-known domain or two letter " + "country in the field "+displayName+"!");
        return false;
  }
  if (len < 2)
  {
        alert("ValidationErr- The address is missing a hostname in the field "+displayName+"!");
        return false;
  }
  return true;
}

function findDayMax(month,year)
{ 
  var modmonth = (month.substring(0,1) == 0)?month.substring(1,2):month; 
  modmonth -= 1;
  nonlyDayMax = [31,28,31,30,31,30,31,31,30,31,30,31];
  lyDayMax = [31,29,31,30,31,30,31,31,30,31,30,31];
  if ((year % 4) == 0)
  {
     if ((year % 100) == 0 && (year % 400) != 0)   
       return nonlyDayMax[modmonth];
     
     return lyDayMax[modmonth];                            
  }
  return nonlyDayMax[modmonth];
}  
function checkValidDateParameters(fromDate,toDate,SysDt)
{
   
   var months=new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
   if(fromDate=="")
   {
         alert("Please, select Action Start Date.");
         return false;
   }
   if(toDate=="")
   {
         alert("Please, select Action Target Date.");  
         return false;
   }   
   var fromDateMonth = fromDate.substr(0, 3);
   var isMonth=False;
   for(var i=0;i<12;i++)
   {
       if (fromDateMonth==months[i])
       {
          isMonth=True; 
          fromDateMonth=""+(i+1);
          break;
       }
   }
   if(!isMonth)
   {
     alert("Please, enter Valid month in Start Date.");
     return false;
   }
   var toDateMonth = toDate.substr(0,3);
   isMonth=False;   
   for(i=0;i<12;i++)
   {   
       if (toDateMonth==months[i])   
       {
          isMonth=True;   
          toDateMonth=""+(i+1);
          break; 
       }
   }
   if(!isMonth)
   {
     alert("Please, enter Valid month in Target Date.");
     return false;
   }
    var fromDateDay = fromDate.substr(4, 2);
    var toDateDay = toDate.substr(4, 2);
    var fromDateYear = fromDate.substr(8, 4);
    var toDateYear = toDate.substr(8, 4);  
    var dayMax = findDayMax(fromDateMonth,fromDateYear);
    if ((fromDateDay < "01") || (fromDateDay > dayMax))
    {
      window.alert("Start Date day value must be between 1 and " + dayMax);   
      return false;
    }      

    dayMax = findDayMax(toDateMonth,toDateYear);
    if ((toDateDay < "01") || (toDateDay > dayMax))
    {
      window.alert("Target Date day value must be between 1 and " + dayMax);
      return false;
    }

    if ((fromDateYear > toDateYear) ||
        ((fromDateYear == toDateYear) && ((fromDateMonth > toDateMonth) ||
                                          ((fromDateMonth == toDateMonth) && (fromDateDay > toDateDay)))))
    {
      window.alert("Target Date must be greater than Start Date.");
      return false;
    }  
	var From_Date = Date.parse(fromDate);
	var now = new Date();
    var Before7Days_Date=now.setDate(now.getDate()-7);
	var diff_date = From_Date - Before7Days_Date;
    if((((diff_date % 31536000000) % 2628000000)/86400000)<0)
    {
        alert("Start Date Should not be less than " + now.toLocaleString());
        return false;
    }
    return true;           
}   
function validateSysDate(fromDate,displayName)
{
   var months=new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
   if(fromDate=="")
   {
         alert("ValidationErr- Please, select date in the field "+displayName+"!");
         return false;
   }  
   var fromDateMonth = fromDate.substr(0, 3);
   var isMonth="False";
   for(var i=0;i<12;i++)
   {
       if (fromDateMonth==months[i])
       {
          isMonth="True"; 
          fromDateMonth=""+(i+1);
          break;
       }
   }
   if(isMonth=="False")
   {
         alert("ValidationErr- Please, enter valid month in the field "+displayName+"!");
         return false;
   }
    var fromDateDay = fromDate.substr(4, 2);
    var fromDateYear = fromDate.substr(8, 4);
    var dayMax = findDayMax(fromDateMonth,fromDateYear);
    if ((fromDateDay < "01") || (fromDateDay > dayMax))
    {
          window.alert("ValidationErr- Day value in the field "+displayName+" must be between 1 and " + dayMax);   
          return false;
    } 
    var From_Date = Date.parse(fromDate);
	var now = new Date();
    var diff_date = From_Date - now;
    if((((diff_date % 31536000000) % 2628000000)/86400000)<0)
    {
		alert("ValidationErr- Date in the field "+displayName+" should not be less than " + now.toLocaleString());
        return false;
    }     
    return true;           
}
function validateDates(fromDate)
{
   try
   {
       var months=new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
       if(fromDate=="")
       {
             alert("ValidationErr- Please, enter date!");
             return false;
       }  
       var fromDateMonth = fromDate.substr(0, 3);
       var isMonth="False";
       for(var i=0;i<12;i++)
       {
           if (fromDateMonth==months[i])
           {
              isMonth="True"; 
              fromDateMonth=""+(i+1);
              break;
           }
       }
       if(isMonth=="False")
       {
             alert("ValidationErr- Please, enter valid month!");
             return false;
       }
        var fromDateDay = fromDate.substr(4, 2);
        var fromDateYear = fromDate.substr(8, 4);
        var dayMax = findDayMax(fromDateMonth,fromDateYear);
        if ((fromDateDay < "01") || (fromDateDay > dayMax))
        {
              window.alert("ValidationErr- Day value must be between 1 and " + dayMax);   
              return false;
        } 
        var From_Date = Date.parse(fromDate);
	    return true;   
	}
	catch(Exc)        {return false;}
}
function validateXmlIE(name,content)
{
	   var valid = true;
	   var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
           xmlDoc.async = "false";
	   xmlDoc.validateOnParse = false;
	   xmlDoc.resolveExternals = false;
	   xmlDoc.loadXML(content);
	   if(xmlDoc.parseError.errorCode != 0)  
			 valid = false;
	   return valid;
}
function validateXmlMozilla(name,content)
{
	   var valid = true;
	   var parser = new DOMParser();
	   var xmlDoc = parser.parseFromString(content,"text/xml");
	   var roottag = xmlDoc.documentElement;
	   if (roottag.tagName == "parsererror")
	       valid = false;
	   return valid;
}	  
function validateNumberRange(control)
{
       try
       {
            var displayName=control.attributes("display-name").value;
            var minVal=parseInt(control.attributes("minValue").value);
            var maxVal=parseInt(control.attributes("maxValue").value);
            if(isInteger(control.value))
            {
		        var number = parseInt(control.value);
		        if( minVal <= number && maxVal>= number)
			        return true;
		        else
		        {
	                  alert("ValidationErr- Not a valid number! "+number+" should be in "+minVal+" to "+maxVal+" range for  \""+displayName+"\" field!");
		              return false;
		        }
	        }
	        else
	        {
	           alert("ValidationErr- Not a valid number! Please enter a valid number for \""+displayName+"\" field!");	
	           return false;
	        }  
       }
       catch(exc){return true;}
}
function maxSize(control)
{
    try
    {
        var displayName=control.attributes("display-name").value;
        var maxSize=parseInt(control.attributes("maxSize").value);
        if(maxSize>0)
        {
           if(control.value.length>maxSize)
           {
              alert("ValidationErr- maximum size of " + displayName + " field is " + maxSize + ".");
              return false;
           } 
        }
        return true;
    }
    catch(exc){return true;}
}  
function minSize(control)
{
    try
    {
        var displayName=control.attributes("display-name").value;
        var minSize=parseInt(control.attributes("minSize").value);
        if(minSize>-1)
        {
           if(control.value.length<minSize)
           {
              alert("ValidationErr- minimum size of " + displayName + " field is " + minSize + ".");
              return false;
           } 
        }
        return true;
    }
    catch(exc){return true;}
}  
function isMandatory(control)
{   
    try
    {  
        var displayName;
        var isMandatory=null;
        var conID=null;
        var isWebContol=false;
        if((control.type.toLowerCase()=="checkbox")||(control.type.toLowerCase()=="radio"))
        {
              var conList=(control.id).split("_");
              if((conList!=null)&&(conList.length>1)&&(!isNaN(conList[conList.length-1])))
              {
                 if(document.getElementById((control.id).substring(0,(control.id).lastIndexOf("_")))!=undefined)
                 {
                     conID=document.getElementById((control.id).substring(0,(control.id).lastIndexOf("_")));
                     isWebContol=true;
                 }
                 else
                     conID=document.getElementById(control.id);
                 displayName=conID.attributes("display-name").value;
                 isMandatory=conID.attributes("isMandatory").value;
              }
              else
                 displayName=control.attributes("display-name").value
        }
        else
            displayName=control.attributes("display-name").value;
            
        if(isMandatory==null)
            isMandatory=control.attributes("isMandatory").value;
            
        if(isMandatory.toLowerCase()=="true")
        {     
            switch(control.type.toLowerCase())
            { 
                case "text"             :
                case "textarea"         :if(isEmpty(control.value))
                                         {
                                              alert("ValidationErr- Enter value for the '" + displayName + "' field.");
                                              return false;
                                         } 
                                         break;
                case "select-one"       :
                case "select-multiple"  :if((control.value=="")||(control.value=="---SELECT ONE---")) 
                                         {
                                              alert("ValidationErr- Select value for the '" + displayName + "' field.");
                                              return false;
                                         }
                                         break;
                case "checkbox"         :
                case "radio"            :var isChecked=false;
                                         if((conID!=null)&&(isWebContol))
                                         {
                                             for (var radioIndex=0;true;radioIndex++)
                                             {
                                                    if(document.getElementById(conID.id+"_" +radioIndex)==undefined)
                                                         break;
                                                    else
                                                        if (document.getElementById(conID.id+"_" +radioIndex).checked)
	   										                 isChecked=true;
                                             }
                                             if(!isChecked)  
                                             {
                                                  alert("ValidationErr- Select value for the '" + displayName + "' field.");
                                                  return false;
                                             }
                                         }
                                         else
                                         {
                                                 var radio = document.getElementsByName(control.name);
                                                 for (var radioIndex=0;radioIndex<radio.length;radioIndex++)
	   										        if (radio[radioIndex].checked)
	   										            isChecked=true;
                                                 if(!isChecked)  
                                                 {
                                                      alert("ValidationErr- Select value for the '" + displayName + "' field.");
                                                      return false;
                                                 }
                                          }
                                          break;
            }
           
        }
        return true;
    }
    catch(exc){return true;}
}
function specialValidator(control)
{
   try
   {
        var displayName=control.attributes("display-name").value;
        var speValid=control.attributes("specialValidations").value.split("~");
        if(speValid.length>0)
        {
            for(var speValidIndex=0;speValidIndex<speValid.length;speValidIndex++)
            {
                switch(speValid[speValidIndex].toLowerCase())
                {
                    case "email"        :if(!validateEmail(control.value,displayName))           return false; break;
                    case "ssn"          :if(!validateSSN(control.value,displayName))             return false; break;
                    case "phoneno"      :if(!validatePhoneNumber(control.value,displayName))     return false; break;
                    case "postalcode"   :if(!validatePostalCode(control.value,displayName))      return false; break;
                    case "creditcard"   :if(!validateCreditCard(control.value,displayName))      return false; break;
                    case "url"          :if(!validateURL(control.value,displayName))             return false; break;
                    case "currency"     :if(!validateCurrency(control.value,displayName))        return false; break;
                    case "currentdate"  :if(!validateSysDate(control.value,displayName))         return false; break;
                    case "numeric"      :if(!validateNumber(control.value,displayName))          return false; break;
                    case "decimal"      :if(!validateDecimal(control.value,displayName))         return false; break;
                    case "int"          :if(!validateInteger(control.value,displayName))         return false; break;
                    case "float"        :if(!validateFloat(control.value,displayName))           return false; break;
                    case "isspecialchar":if(!validateSplChar(control.value,displayName))         return false; break;
               }
            }
        }
        return true;
   }
   catch(exc){return true;}
}

function validateforms() 
{
    for(var conrtolIndex=0; conrtolIndex< document.forms[0].elements.length; conrtolIndex++) 
    {
         if(document.forms[0].elements[conrtolIndex].id != undefined)
         {
             var control = document.forms[0].elements[conrtolIndex];
             if (control.nodeName != "FIELDSET") {
                 switch (control.type.toLowerCase()) {
                     case "text":
                     case "password":
                     case "textarea": if ((!isMandatory(control)) || (!maxSize(control)) || (!minSize(control)) || (!specialValidator(control)) || (!validateNumberRange(control))) return false; break;
                     case "select-one":
                     case "select-multiple": if (!isMandatory(control)) return false; break;
                     case "checkbox":
                     case "radio": if (!isMandatory(control)) return false; break;
                     case "submit": break;
                     case "button": break;
                 }
             }
         }
    }
    return true;
}

function checkRound(intvalue) {
    var flg = 0;
    var ch;
    var v;
    var ReturnVal;
    var st = intvalue.toString();
    for (var index = 0; index < st.length; index++) {

        ch = st.charAt(index);
        if (ch == ".") {
            var ch1 = st.substring(st.indexOf(".") + 1, st.length);
            if (ch1.length > 2) {
                flg = 4;
            }
            if (ch1.length == 2) {
                flg = 3;
            }
            if (ch1.length == 1) {
                flg = 2;
            }
        }
    }

    if (flg == 0) {
        ReturnVal = st + ".00";
    }
    if (flg == 2) {
        ReturnVal = st + "0";
    }
    if (flg == 3) {
        ReturnVal = st;
    }
    if (flg == 4) {
        ReturnVal = roundVal(parseFloat(st)).toString();
    }

    return ReturnVal;
}

function roundVal(val) {
    var dec = 0;
    var result = Math.round(val * Math.pow(10, dec)) / Math.pow(10, dec);
    return result;
}
function validateSplChar(str, displayName) {
    try {
        var spchar, getChar, SpecialChar;
        spchar = "`~&|";
        getChar = 'Empty';
        SpecialChar = 'No';
        var spchars = "`~&|";
        for (var i = 0; i < str.length; i++) {
            for (var j = 0; j < spchar.length; j++) {
                if (str.charAt(i) == spchar.charAt(j)) {
                    SpecialChar = 'Yes';
                    break;
                }
                else {
                    if (str.charAt(i) != ' ')
                        getChar = 'Normal';
                }
            }
        }
        if (SpecialChar == 'Yes') {
            alert("ValidationErr- " + displayName + " field contains special characters.");
            return false;
        }
        return true;
    }
    catch (Exc) { return false; }
}
function chkSplCharacters1(fieldName)
{
 try
    { 
           var charCode = (event.which) ? event.which : event.keyCode;
            if (!((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 123)) && charCode!=13 && charCode!=44 && charCode!=46)
            {	
                alert("Special Characters not Allowed,Kindly Enter Valid "+fieldName+"...!");
                event.keyCode = 0;							
                return false
            }
            return true
     }
 catch(Exc)
    {
     return false;
    }       
}
function chkSplCharacters(fieldName)
{
 try
    { 
           var charCode = (event.which) ? event.which : event.keyCode;
           if(charCode==38 || charCode==39 || charCode==34 || charCode==60 || charCode==62) //38=&,39=',34=",60=<,62=>
           {
            alert("Special Characters(\",\',&,<,>) not Allowed,Kindly Enter Valid "+fieldName+"...!");
            event.keyCode = 0;							
            return false
           }
//            if (!((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 123)) && charCode!=13 && charCode!=44 && charCode!=46 && charCode != 32)
//            {	
//                alert("Special Characters not Allowed,Kindly Enter Valid "+fieldName+"...!");
//                event.keyCode = 0;							
//                return false
//            }
            return true
     }
 catch(Exc)
    {
     return false;
    }       
}
function chkSplCharactersPaste(cid,fieldName)
{
 try
    { 
           var otxt=document.getElementById(cid); 

           var val=document.getElementById(cid).value;

           for(i=0;i<val.length;i++)
           {
               var charCode=val.charCodeAt(i);
               if(charCode==38 || charCode==39 || charCode==34 || charCode==60 || charCode==62)
               {
                alert("Special Characters(\",\',&,<,>) not Allowed,Kindly Enter Valid "+fieldName+"...!");
                document.getElementById(cid).value="";						
                return false
               }
           }
           return true

     }
 catch(Exc)
    {
     return false;
    }       
}
function chkSplChar(fieldName) {
    try {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (!((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 123)) && charCode != 13 && charCode != 44 && charCode != 46 && charCode != 32) {
            alert("Special Characters not Allowed,Kindly Enter Valid " + fieldName + "...!");
            event.keyCode = 0;
            return false
        }
        return true
    }
    catch (Exc) {
        return false;
    }
}
function validate(eve,myid)
{
		
    var charCode = (eve.which) ? eve.which : event.keyCode;
    if ((charCode > 31 && (charCode < 48 || charCode > 57))&& charCode != 46)
    {	
        alert("Please Enter numeric value.");
        event.keyCode = 0;							
        return false
    }
    return true		
}
//to validate selected date should not be greater than current date
function ValidateCurrentDate(id)
{
          //var selectedDate = new Date(document.getElementById(id).value);
          var selectedDate= customParse(document.getElementById(id).value);
          
          var today = new Date();
          today.setHours(0,0,0,0);
          if (today>selectedDate)
          {
                alert('Date Should Be Greater Than Current Date!');
                document.getElementById(id).value = "";
                document.getElementById(id).focus();
                return false;
          }
          return true
}
function customParse(str) {
  var months = ['Jan','Feb','Mar','Apr','May','Jun',
                'Jul','Aug','Sep','Oct','Nov','Dec'],
      n = months.length, re = /(\d{2})-([a-z]{3})-(\d{4})/i, matches;

  while(n--) { months[months[n]]=n; } // map month names to their index :)

  matches = str.match(re); // extract date parts from string

  return new Date(matches[3], months[matches[2]], matches[1]);
}
//to round the Value
function RoundValues(id)
{
   document.getElementById(id).value= checkRound(document.getElementById(id).value);
}
function checkRound(intvalue) {
    var flg = 0;
    var ch;
    var v;
    var ReturnVal;
    var st = intvalue.toString();
    for (var index = 0; index < st.length; index++) {

        ch = st.charAt(index);
        if (ch == ".") {
            var ch1 = st.substring(st.indexOf(".") + 1, st.length);
            if (ch1.length > 2) {
                flg = 4;
            }
            if (ch1.length == 2) {
                flg = 3;
            }
            if (ch1.length == 1) {
                flg = 2;
            }
        }
    }

    if (flg == 0) {
        ReturnVal = st + ".00";
    }
    if (flg == 2) {
        ReturnVal = st + "0";
    }
    if (flg == 3) {
        ReturnVal = st;
    }
    if (flg == 4) {
        ReturnVal = roundVal(parseFloat(st)).toString();
    }
    if(ReturnVal==".00")
    {
        ReturnVal="";
    }
    //ReturnVal = roundVal(parseFloat(intvalue)).toString();
    return ReturnVal;
}
function roundVal(val)
{   
    var dec = 2;
    var result = Math.round(val*Math.pow(10,dec))/Math.pow(10,dec);
    return result;
}
function validationPancard(id) {
    var PanNo = document.getElementById(id).value;
    var regex1 = /^[A-Z]{5}\d{4}[A-Z]{1}$/;  //this is the pattern of regular expersion
    if (regex1.test(PanNo) == false) {
        alert('Please enter valid pan number');
        document.getElementById(id).focus();
        return false;
    }
}
function validateSplCharPaste(controlId) {
    try {
        var spchar, getChar, SpecialChar;
        spchar = "'~<>#&@";
        getChar = 'Empty';
        SpecialChar = false;
        var str = document.getElementById(controlId).value;
        for (var i = 0; i < str.length; i++) {
            for (var j = 0; j < spchar.length; j++) {
                if (str.charAt(i) == spchar.charAt(j)) {
                    SpecialChar = true;
                    break;
                }
                else {
                    if (str.charAt(i) != ' ')
                        getChar = 'Normal';
                }
            }
        }
        if (SpecialChar) {
            str = str.slice(0, str.length - 1);

            alert("ValidationErr - Special characters '~<>#&@' not allowed!");
            document.getElementById(controlId).value = "";
            return false;
        }
        else {
            return true;
        }
    }
    catch (Exc) { return false; }
}

function validateWebSiteURL(id) {
    var urlToValidate = document.getElementById(id).value;
    if (urlToValidate.trim() != "") {
        var myRegExp = /^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/;
        if (!myRegExp.test(urlToValidate)) {
            alert("Validation Error: Kindly enter valid Website URL!!");
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
            return false;
        }
    }
    return true;
}