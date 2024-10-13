/*
 * Author: [Dorey, Dylan]
 * Last Updated: [10/13/2024]
 * [IPlayerState interface instances must implement Handle()]
 */

/// <summary>
/// IPlayerState interface instances must implement Handle()
/// </summary>
public interface IPlayerState
{
    //Passing an instance of PlayerController in the Handle function
    void Handle(PlayerController playerCcontroller);
}
