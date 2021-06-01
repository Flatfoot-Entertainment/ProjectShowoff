// ${DATE} because Rider is ass

public class GitSettings
{
	private static readonly string[] ForbiddenExtensions =
	{
		".unity",
		".prefab"
	};
	
	public static bool IsLockableExtension(string ext)
    {
    	foreach (string extension in ForbiddenExtensions)
    		if (extension == ext) return true;
    	return false;
    }
}