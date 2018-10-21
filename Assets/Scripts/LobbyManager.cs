using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : NetworkLobbyManager {

	public void CreateLobby(string matchName) {
		StartMatchMaker();
            matchMaker.CreateMatch(
                matchName,
                (uint)maxPlayers,
                true,
				"", "", "", 0, 0,
				OnMatchCreate);

            // backDelegate = StopHost;
           	// _isMatchmaking = true;
            // DisplayIsConnecting();

            // SetServerInfo("Matchmaker Host", lobbyManager.matchHost);
	}
}
