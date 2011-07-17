<?php
$dbserver = "127.0.0.1";
$dbuser = "myseesmartled";
$dbpassword = "Mss1234$";
$dbname = "myseesmartled";
$dbconnection = NULL;

ExecuteReader("SELECT {0}", 1);
?>
<?php
function ExecuteReader()
{
	global $dbserver, $dbuser, $dbpassword, $dbname;
	global $dbconnection;

	$sql = FillParams(func_get_args());

	if (is_null($dbconnection))
	{
		$connection = mysql_connect($dbserver, $dbuser, $dbpassword);
		mysql_select_db($dbname);
	}
	$result = mysql_query($sql);
}

function FillParams($arguments)
{
	$arguments = $arguments;
	$argumentCount = count($arguments);
	$sql = $arguments[0];

	if ($argumentCount > 1)
	{
		for ($i = 1; $i < $argumentCount; $i++)
		{
			if (gettype($arguments[$i]) == "string")
			{
				$arguments[$i] = mysql_real_escape_string($arguments[$i]);
			}
			$sql = str_replace("{" . ($i - 1) . "}", $arguments[$i], $sql);
		}
	}
	return $sql;
}
?>