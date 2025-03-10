# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Fixed
- Analyzer messages

### Added
- Support for .NET Standard 2.0
- Support for .NET Standard 2.1

### Changed
- Bump coverlet.msbuild from 6.0.3 to 6.0.4
- Bump coverlet.collector from 6.0.3 to 6.0.4
- Bump CSharpFunctionalExtensions from 3.4.3 to 3.5.1
- Bump Microsoft.NET.Test.Sdk from 17.12.0 to 17.13.0
- Bump MSTest.TestAdapter from 3.7.0 to 3.8.2
- Bump MSTest.TestFramework from 3.7.0 to 3.8.2
- Bump Newtonsoft.Json from 13.0.3 to 13.0.1
- Bump ReportGenerator from 5.4.3 to 5.4.4
- Bump Roslynator.Analyzers from 4.12.10 to 4.13.1
- Bump Roslynator.CodeAnalysis.Analyzers from 4.12.10 to 4.13.1
- Bump Roslynator.Formatting.Analyzers from 4.12.10 to 4.13.1

### Removed
- Support for .NET Framework 4.6.1 because it is added by .NET Standard 2.0
- Support for .NET 5 because it is no longer supported

## [1.2.6] - 2025-01-01

### Changed
- Do not expose Roslynator.Analyzers to parent projects

## [1.2.5] - 2024-12-30

### Fixed
- Version number

### Changed
- Bump Ardalis.GuardClauses from 4.6.0 to 5.0.0
- Bump Microsoft.NET.Test.Sdk from 17.10.0 to 17.12.0
- Bump MSTest.TestAdapter from 3.5.0 to 3.7.0
- Bump MSTest.TestFramework from 3.5.0 to 3.7.0
- Bump ReportGenerator from 5.3.8 to 5.4.3
- Bump Microsoft.CodeAnalysis.NetAnalyzers from 8.0.0 to 9.0.0
- Bump Roslynator.Analyzers from 4.12.4 to 4.12.10
- Bump Roslynator.CodeAnalysis.Analyzers from 4.12.4 to 4.12.10
- Bump Roslynator.Formatting.Analyzers from 4.12.4 to 4.12.10

## [1.2.4] - 2024-07-26

### Fixed
- Code Analysis messages

### Added
- Github workflow for automatic test, build, validate and publish 
- Support for .NET 8.0 
- Roslynator 

### Changed
- Bump Ardalis.GuardClauses from 4.0.1 to 4.6.0
- Bump coverlet.collector from 3.1.2 to 6.0.2
- Bump coverlet.msbuild from 3.1.2 to 6.0.2
- Bump CSharpFunctionalExtensions from 2.29.4 to 2.42.5
- Bump KeepAChangeLogParser from 1.2.2 to 1.2.3
- Bump MSTest.TestAdapter from 2.2.10 to 3.5.0
- Bump MSTest.TestFramework from 2.2.10 to 3.5.0
- Bump Microsoft.CodeAnalysis.PublicApiAnalyzers from 3.3.3 to 3.3.4
- Bump Microsoft.NET.Test.Sdk from 17.2.0 to 17.10.0
- Bump Microsoft.SourceLink.GitHub from 1.1.1 to 8.0.0
- Bump Microsoft.Xaml.Behaviors.Wpf from 1.1.39 to 1.1.122
- Bump Newtonsoft.Json from 13.0.1 to 13.0.3
- Bump ReportGenerator from 5.1.9 to 5.1.18
- Bump Roslynator.Analyzers from 4.0.2 to 4.12.4
- Bump Roslynator.CodeAnalysis.Analyzers from 4.0.2 to 4.12.4
- Bump Roslynator.Formatting.Analyzers from 4.0.2 to 4.12.4
- Bump SimpleInjector from 5.3.3 to 5.4.6

## [1.2.3] - 2022-06-25

### Changed
- Upgrade target framework .NET 4.5.2 to .NET 4.6.2
- Bump SimpleInjector from 5.3.2 to 5.3.3
- Bump coverlet.collector from 3.1.0 to 3.1.2
- Bump MSTest.TestFramework from 2.2.8 to 2.2.10
- Bump Ardalis.GuardClauses from 4.0.0 to 4.0.1
- Bump MSTest.TestAdapter from 2.2.8 to 2.2.10
- Bump Microsoft.NET.Test.Sdk from 17.0.0 to 17.2.0
- Bump coverlet.msbuild from 3.1.0 to 3.1.2
- Bump ReportGenerator from 5.0.2 to 5.1.9
- Bump CSharpFunctionalExtensions from 2.27.1 to 2.29.4
- Bump KeepAChangeLogParser from 1.2.1 to 1.2.2

## [1.2.2] - 2022-01-20

### Changed
- Bump ReportGenerator from 5.0.0 to 5.0.2 
- Bump CSharpFunctionalExtensions from 2.27.0 to 2.27.1 
- Bump Ardalis.GuardClauses from 3.3.0 to 4.0.0 

## [1.2.1] - 2021-12-24

### Changed
- Bump CSharpFunctionalExtensions dependency from 2.26.1 to 2.27.0

## [1.2.0] - 2021-12-22

### Added
- Support for .NET 6.0

## [1.1.1] - 2021-12-22

### Fixed
- Nuget package version

## [1.1.0] - 2021-12-22

### Added
- Automatic line ending determination

### Fixed
- Update dependencies
- Fix analyzer messages

## [1.0.0] - 2021-04-03

### Added
- First Release