BINDIR=index-tool/bin/Debug/
EXE=$(BINDIR)MDData.Index.exe
SOURCES=index-tool/*.cs index-tool/*/*.cs

all: update-index

update-index: $(EXE)
	cd index; mono ../$(EXE)

$(EXE): $(SOURCES)
	cd index-tool; xbuild MDData.Index.sln
