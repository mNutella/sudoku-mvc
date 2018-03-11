namespace Sudoku.Generator {

	class SudokuWorker {

		protected Messenger messenger;

		public SudokuWorker(ref Messenger mess) {
			messenger = mess;
		}

		public void work() {
			bool cont = true;
			int max = messenger.getMaxPuzzles();
			while (cont) {
				BoardGenerator gen = new BoardGenerator();
				gen.generateSolutionBoard();
				gen.generatePuzzleBoard();
				int count = messenger.addPuzzle(gen.getSolutionBoard().toString(), gen.getPuzzleBoard().toString());
				if (count >= max) {
					cont = false;
				}
			}
		}

	}

}