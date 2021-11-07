namespace OBSIntegrations {
    enum OIActionType {
        SetCurrentScene
    }

    enum OIEventType {
        SongStarted, // game scene loaded
        SongFinished, // menu scene loaded
        SongFailed,
        SongCleared,
        SongPaused,
        SongUnpaused
    }
}