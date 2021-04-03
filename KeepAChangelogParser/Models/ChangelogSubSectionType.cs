﻿namespace KeepAChangelogParser.Models
{

  public enum ChangelogSubSectionType
  {
    Added, // for new features
    Changed, // for changes in existing functionality
    Deprecated, // for soon-to-be removed features
    Removed, // for now removed features
    Fixed, // for any bug fixes
    Security, // in case of vulnerabilities
  }

}
